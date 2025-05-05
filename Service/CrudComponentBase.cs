using Microsoft.AspNetCore.Components;
using ProtelAppT.Repositories;

namespace ProtelAppT.Service
{
    public class CrudComponentBase<T> : ComponentBase where T : class, new()
    {
        [Inject] protected IGenericRepository Repo { get; set; }
        [Inject] protected NavigationManager Nav { get; set; }

        [Parameter] public int? Id { get; set; }

        protected T Entity = new();

        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                var existing = await Repo.GetByIdAsync<T>(Id.Value);
                if (existing is not null)
                    Entity = existing;
            }
        }

        protected async Task Save()
        {
            if (Id.HasValue)
                await Repo.UpdateAsync(Entity);
            else
                await Repo.AddAsync(Entity);

            await Repo.SaveChangesAsync();
            Nav.NavigateTo("/");
        }

        protected void Cancel()
        {
            Nav.NavigateTo("/");
        }
    }
}
