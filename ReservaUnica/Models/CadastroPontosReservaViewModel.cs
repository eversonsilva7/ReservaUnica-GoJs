using System.Collections.Generic;

namespace ReservaUnica.Models
{
    public class CadastroPontosReservaViewModel
    {
        /// <summary>
        /// Id que engloba todos os cadastros
        /// </summary>
        public int? Id { get; set; }
        
        /// <summary>
        /// Todos os campos do json para salvar os pontos
        /// </summary>
        public List<NodeDataArray> LstNodeDataArray { get; set; }
    }

    public class NodeDataArray
    {
        /// <summary>
        /// key referente ao indenficador único no GoJs
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Nome do ponto
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Indica se o ponto está ativo
        /// </summary>
        public bool Ativo { get; set; }

        /// <summary>
        /// a coordenada pode conter um decimal de 14 caracteres e vem no formato 477.55919846252567 110.47718947205271 
        /// </summary>
        public string X_Y { get; set; }

        /// <summary>
        /// campo opcional: caso queira guardar a cor do ponto (hexadecimal)
        /// </summary>
        public string Cor { get; set; }

        /// <summary>
        /// campo opcional: Detalhes do ponto
        /// </summary>
        public string Detalhes { get; set; }

        /// <summary>
        /// Coordenada X
        /// </summary>
        public string X { get; set; }

        /// <summary>
        /// Coordenada Y
        /// </summary>
        public string Y { get; set; }
    }
}
