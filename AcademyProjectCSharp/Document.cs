using iTextSharp.text;

namespace AcademyProjectCSharp
{
    internal class Document
    {
        private Rectangle lETTER;
        private int v1;
        private int v2;
        private int v3;
        private int v4;

        public Document(Rectangle lETTER, int v1, int v2, int v3, int v4)
        {
            this.lETTER = lETTER;
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.v4 = v4;
        }
    }
}