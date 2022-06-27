﻿using Entities.Propriedades;

namespace Entities
{
    public class Laboratorio : Empresa
    {
        public Laboratorio()
        {
        }

        public Laboratorio(string razaoSocial, string cNPJ, string nomeContato, string email, string telefone)
        {
            Razao_Social = razaoSocial;
            CNPJ = cNPJ;
            Nome_Contato = nomeContato;
            Email = email;
            Telefone = telefone;
        }
        public override string ToString()
        {
            return this.Razao_Social + "\r\n" +
                   this.CNPJ + "\r\n" +
                   this.Email + "\r\n" +
                   this.Telefone + "\r\n" +
                   this.Ativo + "\r\n" +
                   this.Nome_Contato + "\r\n";
        }
    }
}
