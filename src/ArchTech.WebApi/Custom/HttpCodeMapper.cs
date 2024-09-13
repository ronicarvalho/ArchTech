namespace ArchTech.WebApi.Custom;

public static class HttpCodeMapper
{
    private static readonly Dictionary<int, HttpCodeDescriptor> HttpCodeDictionary = new();

    static HttpCodeMapper()
    {
        AddCode(400, "Requisição Incorreta", "O servidor não pode ou não irá processar a requisição devido a alguma coisa que foi entendida como um erro do cliente.");
        AddCode(401, "Credenciais Inválidas", "A solicitação não foi aplicada porque não possui credenciais de autenticação válidas para o recurso de destino.");
        AddCode(403, "Autorização Proibida", "O servidor entendeu o pedido, mas se recusa a autorizá-lo.");
        AddCode(404, "Recurso não Encontrado", "O servidor não conseguiu encontrar o recurso solicitado.");
        AddCode(405, "Método não Permitido", "Indica que o verbo HTTP utilizado não é suportado.");
        AddCode(406, "Resposta não Aceitável", "O servidor não pode produzir uma resposta que combine com a lista de valores aceitáveis definidas nos cabeçalhos de negociação de conteúdo da requisição proativa.");
        AddCode(407, "Requerida Autenticação de Proxy", "Falta validar as credenciais de autenticação para um servidor proxy que intermedia o navegador e o servidor que pode acessar o recurso solicitado.");
        AddCode(408, "Tempo Limite Expirado", "Indica que a solicitação do cliente não pôde ser concluída dentro do tempo limite estabelecido.");
        AddCode(409, "Conflito", "Há um conflito na solicitação, geralmente devido a uma tentativa de modificar recursos que estão em um estado de conflito.");
        AddCode(410, "Recurso Não Disponível", "Indica que o recurso solicitado não está mais disponível no servidor e foi permanentemente removido.");
        AddCode(411, "Comprimento do Conteúdo Necessário", "Ocorre quando a solicitação não pode ser processada porque o servidor exige que o cliente especifique o comprimento do conteúdo da solicitação.");
        AddCode(412, "Pré-condição Falhou", "Indica que uma ou mais condições prévias especificadas na solicitação não foram atendidas.");
        AddCode(413, "Entidade de Solicitação Muito Grande", "Ocorre quando a solicitação do cliente é rejeitada porque a entidade da solicitação, como um arquivo ou corpo da mensagem, é muito grande para ser processada pelo servidor.");
        AddCode(414, "URI Muito Longa", "Acontece quando a URI na solicitação é muito longa para o servidor processar.");
        AddCode(415, "Tipo de Mídia Não Suportado", "Indica que o servidor não suporta o tipo de mídia ou formato especificado na solicitação.");
        AddCode(416, "Intervalo Não Satisfatório", "Ocorre quando o servidor não pode satisfazer o intervalo de bytes especificado na solicitação.");
        AddCode(417, "Expectativa Falhada", "Indica que a expectativa especificada na solicitação não pôde ser atendida pelo servidor.");
        AddCode(421, "Pedido Direcionado a um Servidor Indefinido", "Indica que a solicitação foi direcionada a um servidor que não está disponível ou não é reconhecido pelo servidor atual.");
        AddCode(422, "Entidade Não Processável", "Usado quando a solicitação está bem formada, mas não pode ser processada devido a erros semânticos, como validação de dados.");
        AddCode(423, "Bloqueado", "Indica que o recurso solicitado está bloqueado para edição e não pode ser modificado até que a condição de bloqueio seja removida.");
        AddCode(424, "Falha na Dependência", "Usado quando a solicitação falhou devido a uma falha na dependência entre recursos, como a necessidade de atualizar recursos relacionados.");
        AddCode(425, "Muito Cedo", "Indica que a solicitação não pode ser processada porque o servidor requer que a solicitação seja repetida em um momento posterior.");
        AddCode(426, "Atualização Requerida", "Usado quando o cliente deve atualizar para continuar a comunicação com o servidor, muitas vezes referindo-se a uma atualização de protocolo.");
        AddCode(428, "Pré-condição Requerida", "Usado quando uma ou mais pré-condições especificadas na solicitação não foram atendidas.");
        AddCode(429, "Taxa de Solicitação Excedida", "Indica que o servidor está recusando a solicitação devido a um número excessivo de solicitações do cliente dentro de um determinado período de tempo.");
        AddCode(431, "Campos de Cabeçalho de Solicitação Muito Grandes", "Ocorre quando o servidor recusa a solicitação devido a cabeçalhos de solicitação muito grandes, o que pode sobrecarregar o servidor.");
        AddCode(451, "Indisponível por Motivos Legais", "Indica que o acesso ao recurso solicitado foi bloqueado devido a restrições legais, como censura governamental.");
        AddCode(499, "Cliente Fechou a Conexão", "Ocorre quando a conexão do cliente com o servidor é encerrada antes de a resposta ser concluída, geralmente indicando um fechamento prematuro da conexão pelo cliente.");
        AddCode(500, "Erro Interno do Servidor", "O erro 500 é um problema interno do servidor, indicando que algo deu errado durante o processamento da solicitação.");
        AddCode(501, "Não Implementado", "O erro 501 indica que o servidor não suporta ou não implementou a funcionalidade na solicitação.");
        AddCode(502, "Gateway Inválido", "O erro 502 ocorre quando o servidor atua como um gateway ou proxy e não pode obter uma resposta válida de outro servidor.");
        AddCode(503, "Serviço Indisponível", "O erro 503 indica que o servidor não pode atender à solicitação no momento devido a sobrecarga temporária ou manutenção.");
        AddCode(504, "Tempo Limite da Porta de Entrada", "O erro 504 ocorre quando o servidor atua como um gateway ou proxy e não consegue obter uma resposta a tempo de um servidor upstream.");
        AddCode(505, "Versão HTTP não suportada", "O erro 505 indica que a versão do protocolo HTTP usada na solicitação não é suportada pelo servidor.");
    }

    private static void AddCode(int statusCode, string shortDescription, string explanation)
    {
        if (HttpCodeDictionary.ContainsKey(statusCode))
            throw new ArgumentException($"Status code {statusCode} already exists in the dictionary.");
        
        HttpCodeDictionary.Add(statusCode, new HttpCodeDescriptor(shortDescription, explanation));
    }
    
    public static HttpCodeDescriptor DescriptorByHttpCode(int httpCode) =>
         HttpCodeDictionary.TryGetValue(httpCode, out var value) 
            ? value : new HttpCodeDescriptor(string.Empty, string.Empty);
    
    public static HttpCodeDescriptor DescriptorByCustomData(string title, string detail) => new(title, detail);
}
