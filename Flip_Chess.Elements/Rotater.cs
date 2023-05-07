namespace Flip_Chess.Elements
{
    internal struct Rotater
    {
        public double Angle;
        public double A0 => Angle;
        public double A1 => Angle + 12.5;
        public double A2 => Angle + 25;
        public double A3 => Angle + 37.5;
        public double A4 => Angle + 50;
        public double A5 => Angle + 62.5;
        public double A6 => Angle + 75;
        public Rotater(double angle)
        {
            this.Angle = angle;
        }
    }
}