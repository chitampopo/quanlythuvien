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
    
    public partial class LOAI_SACH
    {
        public LOAI_SACH()
        {
            this.DAU_SACH = new HashSet<DAU_SACH>();
        }
    
        public int MA_LOAI_SACH { get; set; }
        public string TEN_LOAI_SACH { get; set; }
    
        public virtual ICollection<DAU_SACH> DAU_SACH { get; set; }
    }
}
