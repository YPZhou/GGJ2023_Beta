using System;

namespace GGJ2023.Beta
{
    /// <summary>
    /// 障碍物种类。
    /// </summary>
    public enum ObstacleType
    {
        Laser, //激光
        Block, //路障
        Trap   //陷阱
    }

    /// <summary>
    /// 颜色种类。
    /// </summary>
    public enum ColorType
    {
        Red,
        Blue

    }
    
    
    public static class ConstantExtension
    {
        public static ColorType Reverse(this ColorType colorType)
        {
            return colorType switch
            {
                ColorType.Red => ColorType.Blue,
                ColorType.Blue => ColorType.Red,
                _ => throw new ArgumentOutOfRangeException(nameof(colorType), colorType, null)
            };
        }

    }




}