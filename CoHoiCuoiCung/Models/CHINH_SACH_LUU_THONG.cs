//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoHoiCuoiCung.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CHINH_SACH_LUU_THONG
    {
        public CHINH_SACH_LUU_THONG()
        {
            this.MUON_TRA_SACH = new HashSet<MUON_TRA_SACH>();
        }
    
        public int MA_CHINH_SACH { get; set; }
        public Nullable<int> SO_SACH_MUON_TOI_DA { get; set; }
        public Nullable<int> SO_NGAY_MUON { get; set; }
        public Nullable<int> SO_LAN_DUOC_GIA_HAN { get; set; }
        public Nullable<int> SO_NGAY_GIA_HAN { get; set; }
    
        public virtual ICollection<MUON_TRA_SACH> MUON_TRA_SACH { get; set; }
    }
}
