//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NetworkDatabase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Song
    {
        public Song()
        {
            this.Users = new HashSet<User>();
        }
    
        public int SongId { get; set; }
        public string SongName { get; set; }
        public string Artist { get; set; }
        public int Duration { get; set; }
        public string Url { get; set; }
    
        public virtual ICollection<User> Users { get; set; }
    }
}
