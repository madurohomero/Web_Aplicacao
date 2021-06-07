using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApi_Servidor.Models
{
    public class Aluno
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }

        public List<Aluno> ListarAluno()
        {
            var caminhoArquivo = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            var json = File.ReadAllText(caminhoArquivo);
            var listaAluno = JsonConvert.DeserializeObject<List<Aluno>>(json);
            
            return listaAluno;
        }

        public bool RescreverArquivo(List<Aluno> listaAluno)
        {
            var caminhoArquivo = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            var json = JsonConvert.SerializeObject(listaAluno, Formatting.Indented);
            File.WriteAllText(caminhoArquivo, json);
            return true;
        }

        public Aluno Inserir(Aluno Aluno)
        {
            var listaAluno = this.ListarAluno();

            var maxId = listaAluno.Max(aluno => aluno.id);
            Aluno.id = maxId + 1;
            listaAluno.Add(Aluno);

            RescreverArquivo(listaAluno);
            return Aluno;
        }

        public Aluno Atualizar(int id, Aluno Aluno)
        {
            var listaAlunos = this.ListarAluno();
            var itemIndex = listaAlunos.FindIndex(aluno => aluno.id == id);
            if(itemIndex >= 0)
            {
                Aluno.id = id;
                listaAlunos[itemIndex] = Aluno;
            }
            else
            {
                return null;
            }
            RescreverArquivo(listaAlunos);
            return Aluno;
        }

        public bool Deletar(int id)
        {
            var listaAlunos = this.ListarAluno();
            var itemIndex = listaAlunos.FindIndex(aluno => aluno.id == id);
            if(itemIndex >= 0)
            {
                listaAlunos.RemoveAt(itemIndex);
            }
            else
            {
                return false;
            }
            RescreverArquivo(listaAlunos);
            return true;
        }
    }
}