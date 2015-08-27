using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class ListaEstado
    {     
        public struct EstadosList
        {
            private string _descricao;
            private string _id;

            public EstadosList(string id, string descricao)
            {
                this._id = id;
                this._descricao = descricao;
            }

            public string Id { get { return _id; } }
            public string Descricao { get { return _descricao; } }
        }
        
        public EstadosList[] EstadosLista = new EstadosList[] 
        {
            new EstadosList("AC", "Acre"),
            new EstadosList("AL", "Alagoas"),
            new EstadosList("AP", "Amapá"),
            new EstadosList("AM", "Amazonas"),
            new EstadosList("BA", "Bahia"),
            new EstadosList("CE", "Ceará"),
            new EstadosList("DF", "Distrito Federal"),
            new EstadosList("ES", "Espírito Santo"),
            new EstadosList("GO", "Goiás"),
            new EstadosList("MA", "Maranhão"),
            new EstadosList("MT", "Mato Grosso"),
            new EstadosList("MS", "Mato Grosso do Sul"),
            new EstadosList("MG", "Minas Gerais"),
            new EstadosList("PA", "Pará"),
            new EstadosList("PB", "Paraíba"),
            new EstadosList("PR", "Paraná"),
            new EstadosList("PE", "Pernambuco"),
            new EstadosList("PI", "Piauí"),
            new EstadosList("RJ", "Rio de Janeiro"),
            new EstadosList("RN", "Rio Grande do Norte"),
            new EstadosList("RS", "Rio Grande do Sul"),
            new EstadosList("RO", "Rondônia"),
            new EstadosList("RR", "Roraima"),
            new EstadosList("SC", "Santa Catarina"),
            new EstadosList("SP", "São Paulo"),
            new EstadosList("SE", "Sergipe"),
            new EstadosList("TO", "Tocantins"),
        };
    }
}
