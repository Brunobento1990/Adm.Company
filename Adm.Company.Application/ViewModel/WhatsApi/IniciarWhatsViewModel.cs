﻿namespace Adm.Company.Application.ViewModel.WhatsApi;

public class IniciarWhatsViewModel
{
    public PerfilWhatsViewModel Perfil { get; set; } = null!;
    public IList<ContatoWhatsViewModel> Contatos { get; set; } = [];
}
