using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Normal_Mapping
{
    class Program
    {
        static void Main(string[] args)
        {
            asd.Engine.Initialize("ShaderTest", 800, 600, new asd.EngineOption());

            var obj = new TextureObject2DWithNormalMapping();
            obj.Position = new asd.Vector2DF(100, 100);
            obj.Texture = asd.Engine.Graphics.CreateTexture2D("TestKomarigaoBumpGrayscale.png");
            obj.NormalMap = asd.Engine.Graphics.CreateTexture2DAsRawData("TestKomarigaoNormalMap.png");
            asd.Engine.AddObject2D(obj);

            var obj2 = new TextureObject2DWithNormalMapping();
            obj2.Position = new asd.Vector2DF(300, 100);
            obj2.Texture = asd.Engine.Graphics.CreateTexture2D("Normal_map_example_with_scene_and_result.png");
            obj2.NormalMap = asd.Engine.Graphics.CreateTexture2DAsRawData("Normal_map_example_with_scene_and_result2.png");
            asd.Engine.AddObject2D(obj2);

            var geo = new asd.GeometryObject2D()
            {
                Shape = new asd.CircleShape()
                {
                    InnerDiameter = 0,
                    NumberOfCorners = 8,
                    OuterDiameter = 16,
                },
                Color = new asd.Color(255, 0, 0),
                DrawingPriority = 20,
            };
            asd.Engine.AddObject2D(geo);

            while (asd.Engine.DoEvents())
            {
                asd.Engine.Update();

                var mp = asd.Engine.Mouse.Position;
                geo.Position = mp;

                var lv = new asd.Vector3DF(mp.X, mp.Y, 20);
                obj.LightSource = lv;
                obj.Angle += 0.1f;
                obj2.LightSource = lv;
            }

            asd.Engine.Terminate();
        }
    }
}
