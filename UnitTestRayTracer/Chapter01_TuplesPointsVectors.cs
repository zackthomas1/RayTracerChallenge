using System;
using Xunit;
using RayTracer;

namespace UnitTestRayTracer
{
    public class Chapter01_TuplesPointsVectors
    {
        [Fact]
        public void CompareFloatEquality()
        {
            Assert.True(Utilities.FloatEquality(4000.0f, 4000.0f));
            Assert.False(Utilities.FloatEquality(4000.0f, 5640.0f));
            Assert.False(Utilities.FloatEquality(4000.0f, 4000.0002f));
        }

        [Fact]
        public void TupleEqualiyOperator()
        {
            Point p1 = new Point(-1,5,4);

            Vector3 v1 = new Vector3(4, -7, -3);
            Vector3 v2 = new Vector3(4, -7, -3);
            Vector3 v3 = new Vector3(3, -9, 5);

            RayTracer.Tuple t1 = new RayTracer.Tuple(-1, 5, 4, 1);
            RayTracer.Tuple t2 = new RayTracer.Tuple(3, -9, 5, 0);

            Assert.Equal(t1, p1);
            Assert.True(t1 == p1);
            Assert.Equal(t2, v3);
            Assert.True(t2 == v3);
            Assert.Equal(v1, v2);
            Assert.True(v1 == v2);
        }

        [Fact]
        public void TupleInEqualiyOperator()
        {
            Point p1 = new Point(-1, 5, 4);

            Vector3 v1 = new Vector3(4, -7, -3);
            Vector3 v2 = new Vector3(4, -7, -3);
            Vector3 v3 = new Vector3(3, -9, 5);

            RayTracer.Tuple t1 = new RayTracer.Tuple(-1, 5, 4, 1);
            RayTracer.Tuple t2 = new RayTracer.Tuple(3, -9, 5, 0);

            Assert.NotEqual(t2, p1);
            Assert.True(t2 != p1);
            Assert.NotEqual(t1, v3);
            Assert.True(t1 != v3);
            Assert.NotEqual(v1, v3);
            Assert.True(v1 != v3);
        }

        [Fact]
        public void TuplePoint()
        {
            RayTracer.Tuple t1 = new RayTracer.Tuple(4.3f, -4.2f, 3.1f, 1.0f);

            Assert.Equal(4.3f, t1.x);
            Assert.Equal(-4.2f, t1.y);
            Assert.Equal(3.1f, t1.z);
            Assert.Equal(1.0f, t1.w);

            Assert.True(Utilities.FloatEquality(4.3f, t1.x));
            Assert.True(Utilities.FloatEquality(-4.2f, t1.y));
            Assert.True(Utilities.FloatEquality(3.1f, t1.z));
            Assert.True(Utilities.FloatEquality(1.0f, t1.w));
        }

        [Fact]
        public void TupleVector()
        {
            RayTracer.Tuple t1 = new RayTracer.Tuple(4.3f, -4.2f, 3.1f, 0.0f);

            Assert.Equal(4.3f, t1.x);
            Assert.Equal(-4.2f, t1.y);
            Assert.Equal(3.1f, t1.z);
            Assert.Equal(0.0f, t1.w);

            Assert.True(Utilities.FloatEquality(4.3f, t1.x));
            Assert.True(Utilities.FloatEquality(-4.2f, t1.y));
            Assert.True(Utilities.FloatEquality(3.1f, t1.z));
            Assert.True(Utilities.FloatEquality(0.0f, t1.w));
        }

        [Fact]
        public void AddTuples()
        {
            RayTracer.Tuple t1 = new RayTracer.Tuple(3.0f, -2.0f, 5.0f, 1.0f);
            RayTracer.Tuple t2 = new RayTracer.Tuple(-2.0f, 3.0f, 1.0f, 0.0f);

            RayTracer.Tuple additionTuple = new RayTracer.Tuple();
            RayTracer.Tuple correctAnswerTuple = new RayTracer.Tuple(1, 1, 6, 1);

            additionTuple = t1 + t2;

            Assert.Equal(additionTuple, correctAnswerTuple);
            Assert.True(additionTuple == correctAnswerTuple);

            Assert.True(Utilities.FloatEquality(correctAnswerTuple.x, additionTuple.x));
            Assert.True(Utilities.FloatEquality(correctAnswerTuple.y, additionTuple.y));
            Assert.True(Utilities.FloatEquality(correctAnswerTuple.z, additionTuple.z));
            Assert.True(Utilities.FloatEquality(correctAnswerTuple.w, additionTuple.w));
        }

        [Fact]
        public void AddVectorVector()
        {
            Vector3 v1 = new Vector3(3, 2, 1);
            Vector3 v2 = new Vector3(5, 6, 7);

            Vector3 additionVector = v1 + v2; ;
            Vector3 correctAnswerVector = new Vector3(8, 8, 8);

            Assert.Equal(correctAnswerVector, additionVector);
            Assert.True(correctAnswerVector == additionVector);

            Assert.True(Utilities.FloatEquality(correctAnswerVector.x, additionVector.x));
            Assert.True(Utilities.FloatEquality(correctAnswerVector.y, additionVector.y));
            Assert.True(Utilities.FloatEquality(correctAnswerVector.z, additionVector.z));
            Assert.True(Utilities.FloatEquality(correctAnswerVector.w, additionVector.w));
        }


        [Fact]
        public void AddPointVector()
        {
            Point p1 = new Point(3, 2, 1);
            Vector3 v2 = new Vector3(5, 6, 7);

            Point additionPoint = p1 + v2;
            Point correctAnswerPoint = new Point(8, 8, 8);

            Assert.Equal(correctAnswerPoint, additionPoint);
            Assert.True(correctAnswerPoint == additionPoint);

            Assert.True(Utilities.FloatEquality(correctAnswerPoint.x, additionPoint.x));
            Assert.True(Utilities.FloatEquality(correctAnswerPoint.y, additionPoint.y));
            Assert.True(Utilities.FloatEquality(correctAnswerPoint.z, additionPoint.z));
            Assert.True(Utilities.FloatEquality(correctAnswerPoint.w, additionPoint.w));
        }


        [Fact]
        public void SubtractTuples()
        {
            RayTracer.Tuple t1 = new RayTracer.Tuple(0.005f, 2.0f, 1.0f, 1.0f);
            RayTracer.Tuple t2 = new RayTracer.Tuple(5000.0f, 6.0f, 7.0f, 1.0f);

            RayTracer.Tuple substractionTuple = new RayTracer.Tuple();
            RayTracer.Tuple correctAnswerTuple = new RayTracer.Tuple(-4999.995f, -4f, -6f, 0f);

            substractionTuple = t1 - t2;

            Assert.Equal(correctAnswerTuple, substractionTuple);
            Assert.True(correctAnswerTuple == substractionTuple);

            Assert.True(Utilities.FloatEquality(correctAnswerTuple.x, substractionTuple.x));
            Assert.True(Utilities.FloatEquality(correctAnswerTuple.y, substractionTuple.y));
            Assert.True(Utilities.FloatEquality(correctAnswerTuple.z, substractionTuple.z));
            Assert.True(Utilities.FloatEquality(correctAnswerTuple.w, substractionTuple.w));
        }

        [Fact]
        public void SubtactPointVector()
        {
            Point p1 = new Point(3.0f, 2.0f, 1.0f);
            Vector3 t2 = new Vector3(5.0f, 6.0f, 7.0f);

            Point substractionPoint = new Point();
            Point correctAnswerPoint = new Point(-2, -4, -6);

            substractionPoint = p1 - t2;

            Assert.Equal(correctAnswerPoint, substractionPoint);
            Assert.True(correctAnswerPoint == substractionPoint);

            Assert.True(Utilities.FloatEquality(correctAnswerPoint.x, substractionPoint.x));
            Assert.True(Utilities.FloatEquality(correctAnswerPoint.y, substractionPoint.y));
            Assert.True(Utilities.FloatEquality(correctAnswerPoint.z, substractionPoint.z));
            Assert.True(Utilities.FloatEquality(correctAnswerPoint.w, substractionPoint.w));
        }

        [Fact]
        public void SubtactPointPoint()
        {
            Point p1 = new Point(3.0f, 2.0f, 1.0f);
            Point p2 = new Point(5.0f, 6.0f, 7.0f);

            Vector3 substractionVector = new Vector3();
            Vector3 correctAnswerVector = new Vector3(-2, -4, -6);

            substractionVector = p1 - p2;

            Assert.Equal(correctAnswerVector, substractionVector);
            Assert.True(correctAnswerVector == substractionVector);

            Assert.True(Utilities.FloatEquality(correctAnswerVector.x, substractionVector.x));
            Assert.True(Utilities.FloatEquality(correctAnswerVector.y, substractionVector.y));
            Assert.True(Utilities.FloatEquality(correctAnswerVector.z, substractionVector.z));
            Assert.True(Utilities.FloatEquality(correctAnswerVector.w, substractionVector.w));
        }

        [Fact]
        public void SubtactVectorVector()
        {
            Vector3 p1 = new Vector3 (3.0f, 2.0f, 1.0f);
            Vector3 p2 = new Vector3 (5.0f, 6.0f, 7.0f);

            Vector3 substractionVector = new Vector3();
            Vector3 correctAnswerVector = new Vector3(-2, -4, -6);

            substractionVector = p1 - p2;

            Assert.Equal(correctAnswerVector, substractionVector);
            Assert.True(correctAnswerVector == substractionVector);

            Assert.True(Utilities.FloatEquality(correctAnswerVector.x, substractionVector.x));
            Assert.True(Utilities.FloatEquality(correctAnswerVector.y, substractionVector.y));
            Assert.True(Utilities.FloatEquality(correctAnswerVector.z, substractionVector.z));
            Assert.True(Utilities.FloatEquality(correctAnswerVector.w, substractionVector.w));
        }

        [Fact]
        public void NegationTuple()
        {
            RayTracer.Tuple zero = new RayTracer.Tuple();
            RayTracer.Tuple v1 = new RayTracer.Tuple(1, -2, 3);

            RayTracer.Tuple negationTuple = new RayTracer.Tuple();
            RayTracer.Tuple anwserTuple = new RayTracer.Tuple(-1, 2, -3);

            negationTuple = -v1;

            Assert.Equal(anwserTuple, negationTuple);
            Assert.True(anwserTuple == negationTuple);
        }

        [Fact]
        public void NegationPoint()
        {
            Point zero = new Point();
            Point p1 = new Point(1, -2, 3);

            Point negationPoint = new Point();
            Point anwserPoint = new Point(-1, 2, -3); 

            negationPoint = -p1;

            Assert.Equal(anwserPoint, negationPoint);
            Assert.True(anwserPoint == negationPoint);
        }

        [Fact]
        public void NegationVector()
        {
            Vector3 zero = new Vector3();
            Vector3 v1 = new Vector3(1, -2, 3);

            Vector3 negationVector = new Vector3();
            Vector3 anwserVector = new Vector3(-1, 2, -3);

            negationVector = -v1;

            Assert.Equal(anwserVector, negationVector);
            Assert.True(anwserVector == negationVector);

            Assert.False(1.0f == negationVector.x);
        }

        [Fact]
        public void ScalarMultiplyTuple()
        {
            RayTracer.Tuple t1 = new RayTracer.Tuple(1, -2, 3, -4);
            float scalar01 = 3.5f;

            RayTracer.Tuple scalarMultipleTuple01 = t1 * scalar01;
            RayTracer.Tuple answerTuple01 = new RayTracer.Tuple(3.5f, -7, 10.5f, -14);

            Assert.Equal(answerTuple01, scalarMultipleTuple01);
            Assert.True(answerTuple01 == scalarMultipleTuple01);

            float scalar02 = 0.5f;

            RayTracer.Tuple scalarMultipleTuple02 = t1 * scalar02;
            RayTracer.Tuple answerTuple02 = new RayTracer.Tuple(0.5f, -1, 1.5f, -2);

            Assert.Equal(answerTuple02, scalarMultipleTuple02);
            Assert.True(answerTuple02 == scalarMultipleTuple02);
        }

        [Fact]
        public void ScalarMultiplyPoint()
        {
            Point p1 = new Point(1, -2, 3);
            float scalar01 = 3.5f;

            Point scalarMultiplePoint01 = p1 * scalar01;
            Point answerPoint01 = new Point(3.5f, -7, 10.5f);

            Assert.Equal(answerPoint01, scalarMultiplePoint01);
            Assert.True(answerPoint01 == scalarMultiplePoint01);

            float scalar02 = 0.5f;

            Point scalarMultiplePoint02 = p1 * scalar02;
            Point answerPoint02 = new Point(0.5f, -1, 1.5f);

            Assert.Equal(answerPoint02, scalarMultiplePoint02);
            Assert.True(answerPoint02 == scalarMultiplePoint02);
        }

        [Fact]
        public void ScalarMultiplyVector()
        {
            Vector3 v1 = new Vector3(1, -2, 3);
            float scalar01 = 3.5f;

            Vector3 scalarMultipleVector01 = v1 * scalar01;
            Vector3 answerVector01 = new Vector3(3.5f, -7, 10.5f);

            Assert.Equal(answerVector01, scalarMultipleVector01);
            Assert.True(answerVector01 == scalarMultipleVector01);

            float scalar02 = 0.5f;

            Vector3 scalarMultipleVector02 = v1 * scalar02;
            Vector3 answerVector02 = new Vector3(0.5f, -1, 1.5f);

            Assert.Equal(answerVector02, scalarMultipleVector02);
            Assert.True(answerVector02 == scalarMultipleVector02);
        }

        [Fact]
        public void DividTuple()
        {
            RayTracer.Tuple t1 = new RayTracer.Tuple(1, -2, 3, -4);
            float scalar01 = 2.0f;

            RayTracer.Tuple DividTuple = t1 / scalar01;
            RayTracer.Tuple answerTuple = new RayTracer.Tuple(0.5f, -1, 1.5f, -2f);

            Assert.Equal(answerTuple, DividTuple);
            Assert.True(answerTuple == DividTuple);
        }

        [Fact]
        public void DividPoint()
        {
            Point p1 = new Point(1, -2, 3);
            float scalar01 = 2.0f;

            Point DividePoint = p1 / scalar01;
            Point answerPoint = new Point(0.5f, -1, 1.5f);

            Assert.Equal(answerPoint, DividePoint);
            Assert.True(answerPoint == DividePoint);
        }

        [Fact]
        public void DividVector()
        {
            Vector3 v1 = new Vector3(1, -2, 3);
            float scalar01 = 2.0f;

            Vector3 DivideVector = v1 / scalar01;
            Vector3 answerVector = new Vector3(0.5f, -1, 1.5f);

            Assert.Equal(answerVector, DivideVector);
            Assert.True(answerVector == DivideVector);
        }

        [Fact]
        public void MagnitudeVector()
        {
            Vector3 v1 = new Vector3(1, 0, 0);
            Vector3 v2 = new Vector3(0, 1, 0);
            Vector3 v3 = new Vector3(0, 0, 1);
            Vector3 v4 = new Vector3(1, 2, 3);
            Vector3 v5 = new Vector3(-1, -2, -3);

            float a1 = v1.Magnitude();
            float a2 = v2.Magnitude();
            float a3 = v3.Magnitude();
            float a4 = v4.Magnitude();
            float a5 = v5.Magnitude();

            Assert.Equal(1, a1);
            Assert.Equal(1, a2);
            Assert.Equal(1, a3);
            Assert.Equal((float)(Math.Sqrt(14)), a4);
            Assert.Equal((float)(Math.Sqrt(14)), a5);

            Assert.True(Utilities.FloatEquality(1, a1));
            Assert.True(Utilities.FloatEquality(1, a2));
            Assert.True(Utilities.FloatEquality(1, a3));
            Assert.True(Utilities.FloatEquality(Math.Sqrt(14), a4));
            Assert.True(Utilities.FloatEquality(Math.Sqrt(14), a5));
        }

        [Fact]
        public void NormalizeVector()
        {
            Vector3 v1 = new Vector3(4, 0, 0);
            Vector3 v2 = new Vector3(1, 2, 3);

            v1.Normalize();
            v2.Normalize(); 

            Vector3 a1 = new Vector3(1, 0, 0);
            Vector3 a2 = new Vector3(0.26726124f, 0.5345225f, 0.8017837f); // (1sqrt(14), 2sqrt(14), 3sqrt(14))

            Assert.Equal(a1, v1);
            Assert.True(a1 == v1);

            Assert.Equal(a2, v2);
            Assert.True(a2 == v2);

            Assert.Equal(1, v1.Magnitude());
            Assert.Equal(1, v2.Magnitude());
        }

        [Fact]
        public void NormalizedVector()
        {
            Vector3 v1 = new Vector3(4, 0, 0);
            Vector3 v2 = new Vector3(1, 2, 3);

            Vector3 resultVector01 = v1.Normalize();
            Vector3 resultVector02 = v2.Normalize();

            // Answers
            Vector3 a1 = new Vector3(1, 0, 0);
            Vector3 a2 = new Vector3(0.26726124f, 0.5345225f, 0.8017837f); // (1sqrt(14), 2sqrt(14), 3sqrt(14))

            Assert.Equal(a1, v1);
            Assert.True(a1 == v1);

            Assert.Equal(a2, v2);
            Assert.True(a2 == v2);

            Assert.Equal(1, v1.Magnitude());
            Assert.Equal(1, v2.Magnitude());
        }

        [Fact]
        public void DotProductVector()
        {
            Vector3 v1 = new Vector3(1, 2, 3);
            Point p2 = new Point(2, 3, 4);

            float dotResult = v1.Dot(p2);
            float answer = 20;

            Assert.Equal(answer, dotResult);
        }

        [Fact]
        public void DotProductStaticVector()
        {
            Vector3 v1 = new Vector3(1, 2, 3);
            Point p2 = new Point(2, 3, 4);

            float dotResult = RayTracer.Vector3.Dot(v1, p2);
            float answer = 20;

            Assert.Equal(answer, dotResult);
        }

        [Fact]
        public void CrossProductVector()
        {
            Vector3 v1 = new Vector3(1, 2, 3);
            Vector3 v2 = new Vector3(2, 3, 4);

            Vector3 crossProductReturnV1V2 = v1.Cross(v2);
            Vector3 crossProductReturnV2V1 = v2.Cross(v1);

            Vector3 answerV1V2 = new Vector3(-1, 2, -1);
            Vector3 answerV2V1 = new Vector3(1, -2, 1);

            Assert.Equal(answerV1V2, crossProductReturnV1V2);
            Assert.True(answerV1V2 == crossProductReturnV1V2);

            Assert.Equal(answerV2V1, crossProductReturnV2V1);
            Assert.True(answerV2V1 == crossProductReturnV2V1);
        }

        [Fact]
        public void CrossProductStaticVector()
        {
            Vector3 v1 = new Vector3(1, 2, 3);
            Vector3 v2 = new Vector3(2, 3, 4);

            Vector3 crossProductReturnV1V2 = Vector3.Cross(v1, v2);
            Vector3 crossProductReturnV2V1 = Vector3.Cross(v2, v1);

            Vector3 answerV1V2 = new Vector3(-1, 2, -1);
            Vector3 answerV2V1 = new Vector3(1, -2, 1);

            Assert.Equal(answerV1V2, crossProductReturnV1V2);
            Assert.True(answerV1V2 == crossProductReturnV1V2);

            Assert.Equal(answerV2V1, crossProductReturnV2V1);
            Assert.True(answerV2V1 == crossProductReturnV2V1);
        }



    }
}
