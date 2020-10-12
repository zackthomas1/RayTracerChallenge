using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Group : RayObject
    {

        public Group() : base()
        {

        }



        public override List<Intersection> LocalIntersects(Ray objSpaceRay)
        {
            throw new NotImplementedException();
        }

        public override Vector3 LocalNormal(Point objectPoint)
        {
            throw new NotImplementedException();
        }
    }
}
