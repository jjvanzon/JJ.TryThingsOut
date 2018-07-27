<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MvcApplication1.Models.AccessoiresMerkViewModel>" %>

<%  if (Model.Artikels.Count != 0)
    { %>
		<div class="choice" id="filterContent">	
			<ul class="Artikelen">
                <% foreach (var artikel in Model.Artikels) { %>
			        <li class="artikel">
				        <a href="^^/<%= Html.Encode(Model.Merk.Naam) %>" title="<%= Html.Encode(Model.Merk.Naam) %> <%= Html.Encode(Model.Merk.NaamCommercieel) %> GSM">
					        <strong><%= Html.Encode(Model.Merk.Naam) %>nbsp;<%= Html.Encode(Model.Merk.NaamCommercieel) %></strong>
				        </a>
				        <ul>
					        <li class="image">
                                <img 
                                    src="~/afbeeldingen/artikel/<%= Html.Encode(Model.Merk.AfbeeldingKlein) %>" 
                                    alt="<%= Html.Encode(Model.Merk.Naam) %> <%= Html.Encode(Model.Merk.NaamCommercieel) %>" />
					        </li>
					        <li class="price">
                                <% if (Model.ModuleID == 16 && artikel.IsActiefInLosseGsm == true) { %>
							            <span>Prijs: <Veld functie="BedragInclusief" format="c2">#PrijsBerekend#</Veld></span>
                                <% } %>
                                <% if (Model.ModuleID == 16 && artikel.IsActiefInLosseGsm == false) { %>
                                    <%  if (artikel.IsActiefInGsmAbo) { %>
								            i.c.m. abonnement
                                    <%  }
                                        else { %>
								            nbsp;
                                    <%  } %>
                                <% } %>
					        </li>
				        </ul>	
			        </li>
                <% } %>
			</ul>
		</div> 
<%  }
    else
    { %>
	    <p class="melding">
            <% if (Model.ModuleID == 128) { %>
			    Er zijn op dit moment geen accessoires beschikbaar voor <%= Html.Encode(Model.Merk.Naam) %> mobiele telefoons.
		    <% } %>
            <% if (Model.ModuleID != 128) { %>
			    Er zijn op dit moment geen <%= Html.Encode(Model.Merk.Naam) %> mobiele telefoons.	
		    <% } %>
	    </p>  
<%  } %>
