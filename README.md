Generative AI com Ollama

### 1. Pré-requisitos
Instalar .NET 8 SDK\
https://dotnet.microsoft.com/download\
Verificar:\
dotnet --version


### 2. Instalar Docker Desktop

https://www.docker.com/products/docker-desktop
Ativar: WSL, Virtualization na BIOS (se necessário) 


### 3. Instalar Ollama
https://ollama.com/download \
Após instalação: \
ollama --version


### 4. Baixar modelos do Ollama

Embeddings\
ollama pull nomic-embed-text

Chat LLM\
ollama pull qwen2.5:7b

após downloads executar:\
ollama list


### 5. Subir Elasticsearch via Docker

docker run -d ^\
  --name elastic ^\
  -p 9200:9200 ^\
  -p 9300:9300 ^\
  -e "discovery.type=single-node" ^\
  -e "xpack.security.enabled=false" ^\
  docker.elastic.co/elasticsearch/elasticsearch:8.12.0


Após subir o container verificar no browser:\
http://localhost:9200


### 6. Criar solução .NET

dotnet new sln -n GenAI\
dotnet new webapi -n GenAI.Api -f net8.0\
dotnet sln add GenAI.Api/GenAI.Api.csproj


### 7. Instalar pacotes NuGet
Dentro da API:
  <ItemGroup>
    <PackageReference Include="Elasticsearch.Net" Version="7.17.5" />
    <PackageReference Include="itext7" Version="9.6.0" />
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="8.0.27" />
    <PackageReference Include="NEST" Version="7.17.5" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.6.2" />
  </ItemGroup>
 
### 8. Rodar tudo localmente
Ordem correta:\
docker start elastic\
ollama serve\
dotnet run\
