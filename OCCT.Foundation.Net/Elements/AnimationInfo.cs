using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OCCT.Foundation.Net.Elements
{
    /// <summary>
    /// 动画信息
    /// </summary>
    public class AnimationInfo
    {
        public AnimationInfo() { }

        /// <summary>
        /// 动画线程名称
        /// </summary>
        public string theAnimationName { get; set; }
        /// <summary>
        /// 动画驱动图形
        /// </summary>
        public string[] theObjects { get; set; }
        ///// <summary>
        ///// 起始转换规则（一个值是旋转，三个值是平移）
        ///// </summary>
        public List<double> theTrsfStart { get; set; } = new List<double>();
        ///// <summary>
        ///// 结束转换规则（一个值是旋转，三个值是平移）
        ///// </summary>
        public List<double> theTrsfEnd { get; set; } = new List<double>();
        /// <summary>
        /// 定义动画的持续时间
        /// </summary>
        public double theDuration { get; set; } = 10.0f;
        /// <summary>
        /// 开始计时器位置（显示时间戳）
        /// </summary>
        public double theStartPts { get; set; } = 0.0f;
        /// <summary>
        /// 播放速度（1.0表示正常速度）
        /// </summary>
        public double thePlaySpeed { get; set; } = 1.0f;
        /// <summary>
        /// 将定义的动画更新到指定开始位置的标志
        /// </summary>
        public bool theToUpdate { get; set; } = true;
        /// <summary>
        /// 在起始位置暂停计时器的标志默认设置为False
        /// </summary>
        public bool theToStopTimer { get; set; } = false;
        /// <summary>
        /// 动画调整数据
        /// </summary>
        public float Value { get; set; }
        /// <summary>
        /// 运动模式
        /// </summary>
        public ActivityType AType { get; set; } = ActivityType.Translation;

        /// <summary>
        /// 旋转轴或者平移向量(6个值，前三个是点，后三个是方向)
        /// </summary>
        public List<double> Axis { get; set; } = new List<double>();

        /// <summary>
        /// 联动元素
        /// </summary>
        public List<AnimationInfo> LinkElements { get; set; } = null;
    }
}
