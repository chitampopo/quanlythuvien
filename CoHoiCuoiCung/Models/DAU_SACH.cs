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
    
    public partial class DAU_SACH
    {
        public DAU_SACH()
        {
            this.CUON_SACH = new HashSet<CUON_SACH>();
            this.TAC_GIA = new HashSet<TAC_GIA>();
            this.KE_SACH = new HashSet<KE_SACH>();
        }
    
        public int MA_DAU_SACH { get; set; }
        public int MA_NGON_NGU { get; set; }
        public int MA_NXB { get; set; }
        public int MA_LOAI_SACH { get; set; }
        public string TEN_DAU_SACH { get; set; }
    
        public virtual ICollection<CUON_SACH> CUON_SACH { get; set; }
        public virtual NGON_NGU NGON_NGU { get; set; }
        public virtual NHA_XUAT_BAN NHA_XUAT_BAN { get; set; }
        public virtual LOAI_SACH LOAI_SACH { get; set; }
        public virtual ICollection<TAC_GIA> TAC_GIA { get; set; }
        public virtual ICollection<KE_SACH> KE_SACH { get; set; }
    }
}
