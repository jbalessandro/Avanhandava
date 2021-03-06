﻿using Avanhandava.Domain.Service.Admin;
using System.Linq;
using System.Web.Mvc;

namespace Avanhandava.Common
{
    public static class BindContaHelper
    {
        public static MvcHtmlString SelectConta(this HtmlHelper html, int idConta = 0, int idEmpresa = 0, bool todas = false)
        {
            var contas = new ContaService().Listar()
                .Where(x => x.Ativo == true &&
                    (idEmpresa == 0 || x.IdEmpresa == idEmpresa))
                .OrderBy(x => x.Descricao)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "IdConta");
            tag.MergeAttribute("name", "IdConta");
            tag.MergeAttribute("class", "form-control");

            if (todas == true)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", "0");
                itemTag.SetInnerText("");
                tag.InnerHtml += itemTag.ToString();
            }

            foreach (var item in contas)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idConta)
                {
                    itemTag.MergeAttribute("selected", "selected");
                }
                itemTag.SetInnerText(item.Descricao);
                tag.InnerHtml += itemTag.ToString();
            }

            return new MvcHtmlString(tag.ToString());
        }
    }
}