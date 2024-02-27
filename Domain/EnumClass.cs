using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public enum EnumDeviceType
    {
        /// <summary>
        /// 智能电动病床
        /// </summary>
        IntelligentElectricMedicalBed = 10003,
        /// <summary>
        /// 无感体征监测垫
        /// </summary>
        SignMonitoringMattress = 10004,
        /// <summary>
        /// 智能防褥疮床垫
        /// </summary>
        IntelligentAntiBedsoreMattress = 10005,
        /// <summary>
        /// 租赁床
        /// </summary>
        RentalBed = 10007
    }
}
