﻿using BusinessLogicalLayer.Verificaçoes;
using DataAccessLayer;
using Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.BusinessLL
{
    public class FuncionarioBll : ICRUD<Funcionario>
    {
        FuncionarioDAL funcionarioDAL = new FuncionarioDAL();
        FuncionarioValidator funcionarioValidator = new FuncionarioValidator();
        public Response Insert(Funcionario item)
        {
            
            Response resposta = funcionarioValidator.Validate(item);
            if (resposta.HasSuccess)
            {
                return funcionarioDAL.Insert(item);
            }
            else { return resposta; }
        }

        public Response Update(Funcionario item)
        {
            Response resposta = funcionarioValidator.Validate(item);
            if (resposta.HasSuccess)
            {
                return funcionarioDAL.Update(item);
            }
            else { return resposta; }
        }
        public Response Delete(int id)
        {
            return funcionarioDAL.Delete(id);
        }

        public DataResponse<Funcionario> GetAll()
        {
            throw new NotImplementedException();
        }

        public SingleResponse<Funcionario> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public SingleResponse<Funcionario> GetByID(int id)
        {
            throw new NotImplementedException();
        }

    }
}