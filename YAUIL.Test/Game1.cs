using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace YAUIL.Test {
    public class Game1:Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        public readonly ElementContainer frame = new();

        private Texture2D _emptyTexture;

        protected override void Initialize() {
            base.Initialize();

            frame.Add(new Element() {
                Name = "Container",
                Area = new(0,0,1,1),
                AreaMode = new() {
                    X = CoordinateMode.ParentRTL,
                    Y = CoordinateMode.ParentRTL,
                    Width = SizeMode.ParentWidth,
                    Height = SizeMode.ParentHeight
                },
                Padding = Padding.All(8),
                ID = 1
            });

            frame.Add(new Element() {
                Name = "Child",
                Area = new(25,25,0.1f,0.2f),
                AreaMode = new() {
                    Width = SizeMode.ViewportHeight,
                    Height = SizeMode.ViewportHeight
                },
                ID = 2,
                ParentID = 1
            });
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _emptyTexture = GetEmptyTexture(8);
        }

        private Texture2D GetEmptyTexture(int size) {
            var emptyTexture = new Texture2D(GraphicsDevice,size,size);
            Color[] colorData = new Color[size * size];
            for(int i = 0;i < colorData.Length;i++) {
                colorData[i] = Color.White;
            }
            emptyTexture.SetData(colorData);
            return emptyTexture;
        }

        private Rectangle GetRectangle(Area area) => new((int)area.X,(int)area.Y,(int)area.Width,(int)area.Height);
        private Area GetViewport(Rectangle rectangle) => new(rectangle.X,rectangle.Y,rectangle.Width,rectangle.Height);

        private readonly Color[] colors = new Color[] {
            Color.Red, Color.Orange, Color.Yellow
        };

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            frame.Area = GetViewport(GraphicsDevice.Viewport.Bounds);
            var layout = frame.GetLayout();

            int colorIndex = 0;
            _spriteBatch.Begin();
            foreach(var element in layout) {
                _spriteBatch.Draw(_emptyTexture,GetRectangle(element.Area),colors[colorIndex=(colorIndex+1)%colors.Length]);
            }
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
