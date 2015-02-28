using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XCollisionLib;

namespace XCollision
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class CollisionTester : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        FRectangle Player;
        FRectangle Collision;
        SpriteFont DebugFont;
        DebugRender DebugRender;

        SpriteBatch SpriteBatch;
        Texture2D Fill;
        Vector2 Direction;
        public CollisionTester()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Player = new FRectangle(32.0f, 32.0f, 32.0f, 32.0f);
            Collision = new FRectangle(256.0f, 256.0f, 460.0f, 320.0f);
            SpriteBatch = new SpriteBatch(this.GraphicsDevice);
            Fill = new Texture2D(this.GraphicsDevice, 1, 1,false,SurfaceFormat.Color);
                    Color[] ColorData = new Color[1];
                    ColorData[0] = Color.White;
            Fill.SetData<Color>(ColorData);
            DebugRender = new DebugRender(GraphicsDevice);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.DebugFont = Content.Load<SpriteFont>("Debug");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            DebugRender.Clear();

            Direction = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left * new Vector2(260.0f, -660.0f) * (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f;

            //DebugRender.AddLine(Player.Center, Player.Center + (Direction*Collision.Inflate(Player.Width, Player.Height).RayTime(Player.Center, Player.Center + Direction)), Color.Blue);
            Player.Size += GamePad.GetState(PlayerIndex.One).ThumbSticks.Right * new Vector2(160.0f, -160.0f) * (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f;
            float ray = Collision.Inflate(Player.Width, Player.Height).RayTime(Player.Center, Player.Center + Direction);

            Player += Direction;// *Math.Min(Math.Max(ray, 0.0f), 1.0f);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch.Begin();
            FRectangle Intersection = Player.Intersection(Collision);

            Color PlayerColor, CollisionColor;

            if (Player.Intersects(Collision)) CollisionColor = Color.Yellow;
            else CollisionColor = Color.Green;

            if (Collision.Contains(Player)) PlayerColor = Color.Magenta;
            else PlayerColor = Color.Red;

            
            SpriteBatch.Draw(Fill, Collision.ToRectangle(), CollisionColor);
            SpriteBatch.Draw(Fill, Player.ToRectangle(), PlayerColor);
            if(Intersection != Player) SpriteBatch.Draw(Fill, Intersection.ToRectangle(), Color.White);

            SpriteBatch.DrawString(DebugFont, "Player : " + Player.ToString(), new Vector2(16.0f, 16.0f), Color.White);
            SpriteBatch.DrawString(DebugFont, "Collision : " + Collision.ToString(), new Vector2(16.0f, 32.0f), Color.White);
            SpriteBatch.DrawString(DebugFont, "Result : " + Collision.Inflate(Player.Width, Player.Height).RayTime(Player.Center, Player.Center + Direction) , new Vector2(16.0f, 48.0f), Color.White);
            
            SpriteBatch.End();

            //DebugRender.Draw();

            base.Draw(gameTime);
        }
    }
}
