using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;

namespace ClubeDaLeituraWeb.WebApp.Compartilhado.Infra.Arquivos;

public abstract class RepositorioBaseEmArquivo<T> where T : EntidadeBase<T>
{
    protected ContextoJson contexto;
    protected List<T> registros;

    public RepositorioBaseEmArquivo(ContextoJson contexto)
    {
        this.contexto = contexto;
        this.registros = CarregarRegistros();
    }

    protected abstract List<T> CarregarRegistros();

    public virtual void Cadastrar(T entidade)
    {
        registros.Add(entidade);

        contexto.Salvar();
    }

    public virtual bool Editar(string idSelecionado, T entidadeAtualizada)
    {
        T? registroSelecionado = SelecionarPorId(idSelecionado);

        if (registroSelecionado == null)
            return false;

        registroSelecionado.AtualizarDados(entidadeAtualizada);

        contexto.Salvar();

        return true;
    }

    public virtual bool Excluir(T registro)
    {
        bool conseguiuExcluir = registros.Remove(registro);

        if (conseguiuExcluir)
            contexto.Salvar();

        return conseguiuExcluir;
    }

    public virtual bool Excluir(string idSelecionado)
    {
        T? registroSelecionado = SelecionarPorId(idSelecionado);

        if (registroSelecionado == null)
            return false;

        return Excluir(registroSelecionado);
    }

    public virtual T? SelecionarPorId(string idSelecionado)
    {
        foreach (T registro in registros)
        {
            if (registro.Id == idSelecionado)
                return registro;
        }

        return null;
    }

    public virtual List<T> SelecionarTodos()
    {
        return registros;
    }

    public virtual List<T> Filtrar(Predicate<T> filtro)
    {
        List<T> registrosFiltrados = new List<T>();

        foreach (T e in registros)
        {
            if (filtro(e))
                registrosFiltrados.Add(e);
        }

        return registrosFiltrados;
    }
}
