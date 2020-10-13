using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer
{
    public class Group : RayObject
    {
        public List<RayObject> childern { get; set; } = new List<RayObject>();

        public Group() : base()
        {

        }

        public void AddChild(RayObject obj)
        {
            obj.parent = this;
            childern.Add(obj);
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
