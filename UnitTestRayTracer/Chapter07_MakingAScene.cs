using System;
using Xunit;
using RayTracer;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic;

namespace UnitTestRayTracer
{
    public class Chapter07_MakingAScene
    {

        [Fact]
        public void SetUpSceneClass()
        {
            Scene sceneDefault = new Scene();

            Material m1 = new Material(new Color(0.8f, 1.0f, 0.6f), diffuse: 0.7f, specular: 0.2f);
            Sphere s1 = new Sphere(m1);
            Sphere s2 = new Sphere(radius: 0.5f);

            Assert.Single(sceneDefault.Lights);
            Assert.Equal(2, sceneDefault.Objects.Count);

            Assert.True(s1 == sceneDefault.Objects[0]);
            Assert.True(s2 == sceneDefault.Objects[1]);
        }
    }
}
