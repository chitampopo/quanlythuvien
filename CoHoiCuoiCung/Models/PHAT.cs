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
    
    public partial class PHAT
    {
        public int MA_MUON_TRA { get; set; }
        public int MA_LY_DO { get; set; }
        public int MA_SACH { get; set; }
        public Nullable<int> SO_TIEN_BI_PHAT { get; set; }
    
        public virtual CUON_SACH CUON_SACH { get; set; }
        public virtual LY_DO_PHAT LY_DO_PHAT { get; set; }
        public virtual MUON_TRA_SACH MUON_TRA_SACH { get; set; }
    }
}
