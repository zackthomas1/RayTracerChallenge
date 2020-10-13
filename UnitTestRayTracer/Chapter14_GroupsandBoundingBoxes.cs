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
            Assert.Empty(g.childern);
        }

        [Fact]
        public void RayObjectHasParent()
        {
            RayObject obj = new Sphere();

            Assert.Null(obj.parent);
        }

        [Fact]
        public void AddingChildToGroup()
        {
            Group g = new Group();
            Sphere s = new Sphere();
            g.AddChild(s);

            Assert.NotEmpty(g.childern);
            Assert.Contains(s, g.childern);
            Assert.True(s.parent == g);
        }

    }
}
