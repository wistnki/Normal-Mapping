using System;
using System.IO;

namespace Normal_Mapping
{
    class TextureObject2DWithNormalMapping : asd.TextureObject2D
    {
        /// <summary>
        /// 光源の座標を取得または設定します。
        /// </summary>
        /// <remarks>Z軸正方向が画面手前方向</remarks>
        public asd.Vector3DF LightSource { get; set; }

        /// <summary>
        /// 法線マップとして用いるテクスチャを取得または設定します。
        /// </summary>
        /// <remarks>設定するテクスチャは<see cref="asd.Engine.Graphics.CreateTexture2DAsRawData">で生成する必要があります。</remarks>
        public asd.Texture2D NormalMap
        {
            get => _NormalMap;
            set
            {
                _Material.SetTexture2D("g_norm", value);
                _Material.SetTextureFilterType("g_norm", TextureFilterType);
                _Material.SetTextureWrapType("g_norm", asd.TextureWrapType.Clamp);

                _NormalMap = value;
            }
        }
        private asd.Texture2D _NormalMap;

        /// <summary>
        /// テクスチャを取得または設定します。
        /// </summary>
        public new asd.Texture2D Texture
        {
            get => _Texture;
            set
            {
                _Material.SetTexture2D("g_texture", value);
                _Material.SetTextureFilterType("g_texture", TextureFilterType);
                _Material.SetTextureWrapType("g_texture", asd.TextureWrapType.Clamp);

                _Material.SetVector2DF("g_texture_size", value.Size.To2DF());

                _Texture = value;
            }
        }
        private asd.Texture2D _Texture;

        private static asd.Shader2D _Shader;
        private asd.Material2D _Material;

        public TextureObject2DWithNormalMapping()
        {
            if (_Shader is null)
            {
                var shaderCode = File.ReadAllText("norm_shader.hlsl");
                _Shader = asd.Engine.Graphics.CreateShader2D(shaderCode);
            }
            _Material = asd.Engine.Graphics.CreateMaterial2D(_Shader);
        }

        protected override void OnDrawAdditionally()
        {
            base.OnDrawAdditionally();

            var t1 = GetGlobalPosition() + (new asd.Vector2DF(0, 0) - CenterPosition);
            var t2 = GetGlobalPosition() + (new asd.Vector2DF(Texture.Size.X, 0) - CenterPosition);
            var t3 = GetGlobalPosition() + (Texture.Size.To2DF() - CenterPosition);
            var t4 = GetGlobalPosition() + (new asd.Vector2DF(0, Texture.Size.Y) - CenterPosition);

            _Material.SetVector3DF("g_light_pos",
                LightSource * new asd.Vector3DF(1, 1, -1) - new asd.Vector3DF(Position.X, Position.Y, 0));

            DrawSpriteWithMaterialAdditionally(
                t1, t2, t3, t4,
                AbsoluteColor, AbsoluteColor, AbsoluteColor, AbsoluteColor,
                new asd.Vector2DF(0.0f, 0.0f),
                new asd.Vector2DF(1.0f, 0.0f),
                new asd.Vector2DF(1.0f, 1.0f),
                new asd.Vector2DF(0.0f, 1.0f),
                _Material, asd.AlphaBlendMode.Opacity, 1);
        }

    }
}
