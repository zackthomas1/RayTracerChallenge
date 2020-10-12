using System;
using Xunit;
using RayTracer;
using RayTracer.RayObjects;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;

namespace UnitTestRayTracer
{
    public class Chapter14_GroupsandBoundingBoxes
    {
        [Fact]
        public void CreateNewGroup()
        {
            Group g = new Group();

            Assert.True(g.Transform == new Matrix4()); 
        }

    }
}
