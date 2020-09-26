﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks.Dataflow;
using System.Windows.Markup;

namespace RayTracer
{
    public abstract class RayObject
    {
        //Instance Variables
        protected static int currentID = 0;
        protected int id;
        Matrix4 transformMatrix = new Matrix4();
        Point position;
        public Material material;

        // Get/Set methods
        public int ID
        {
            get { return id; }
            private set { id = value; }
        }
        public Matrix4 TransformMatrix
        {
            get { return transformMatrix; }
            set { transformMatrix = value; }
        }
        public Point Position
        {
            get { return position; }
            set { position = value; }
        }

        // Constructors
        public RayObject()
        {
            id = currentID++;
            material = new Material();
        }

        public RayObject(Material material)
        {
            id = currentID++;
            this.material = material;
        }

        // Class overloads
        public override string ToString()
        {
            return "RayObject_" + id.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is RayObject @object &&
                   id == @object.id &&
                   EqualityComparer<Matrix4>.Default.Equals(transformMatrix, @object.transformMatrix) &&
                   EqualityComparer<Point>.Default.Equals(position, @object.position) &&
                   EqualityComparer<Material>.Default.Equals(material, @object.material) &&
                   ID == @object.ID &&
                   EqualityComparer<Matrix4>.Default.Equals(TransformMatrix, @object.TransformMatrix) &&
                   EqualityComparer<Point>.Default.Equals(Position, @object.Position);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, transformMatrix, position, material, ID, TransformMatrix, Position);
        }

        public static bool operator ==(RayObject obj1, RayObject obj2)
        {
            if(obj1.Position == obj2.Position && obj1.material == obj2.material)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(RayObject obj1, RayObject obj2)
        {
            if (obj1.Position == obj2.Position && obj1.material == obj2.material)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        // Methods
        public abstract List<Intersection> Intersect(Ray ray);

    }
}
