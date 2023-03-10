using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using YAUIL;
using YAUIL.Layout;

namespace YAUILTest {
    public class Game1:Game {
        private readonly GraphicsDeviceManager _graphics;
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

        private static Rectangle GetRectangle(Area area) => new((int)area.X,(int)area.Y,(int)area.Width,(int)area.Height);
        private static Area GetViewport(Rectangle rectangle) => new(rectangle.X,rectangle.Y,rectangle.Width,rectangle.Height);

        private readonly Color[] colors = new Color[] {
            Color.Red, Color.Orange, Color.Yellow
        };

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            frame.Area = GetViewport(GraphicsDevice.Viewport.Bounds);
            frame.Clear();

            frame.Add(new Element() {
                Name = "container",
                Area = (Size.ParentWidth,Size.ParentHeight),
                Padding = (8,SizeMode.Absolute),
                ID = 1
            });

            frame.Add(new Element() {
                Name = "container-child",
                ID = 2,
                Parent = 1,
                Area = (Coordiante.ParentCenterX,Coordiante.ParentCenterY,50),
                OriginOffset = -0.5f
            });

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
