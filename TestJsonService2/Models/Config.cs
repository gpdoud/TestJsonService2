using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestJsonService2.Models {
    public class Config {

        [Key]
        [StringLength(50)]
        public string Key { get; set; }
        [StringLength(100)]
        public string Value { get; set; }
        [StringLength(30)]
        public string Category { get; set; }
        public bool Active { get; set; } = true;

        public Config() {}
    }
}
