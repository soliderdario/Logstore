﻿using Logstore.Domain.Model;
using Logstore.Domain.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Logdtore.Domain.View
{
    public class OrderView: ViewBase
    {        

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public DateTime DateCreate { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Street { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(10, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Number { get; set; }

        [StringLength(40, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 0)]
        public string Complement { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(40, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(40, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string City { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(9, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 9)]
        public string PostalCode { get; set; }
        
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public List<OrderItemView> Items { get; set; }
    }

    public class OrderItemView : ViewBase
    {
        public List<long> Flavors { get; set; }
    }
}
