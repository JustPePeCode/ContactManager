export function renderContacts(contacts, contactGrid) {
  contactGrid.innerHTML = "";
  contacts.forEach((contact) => {
    const card = document.createElement("div");
    card.classList.add("card");

    const nameP = document.createElement("p");
    nameP.classList.add("name");
    nameP.textContent = contact.name;
    card.appendChild(nameP);

    const emailP = document.createElement("p");
    emailP.classList.add("email");
    emailP.textContent = contact.email;
    card.appendChild(emailP);

    const gsmP = document.createElement("p");
    gsmP.classList.add("gsm");
    gsmP.textContent = contact.gsm;
    card.appendChild(gsmP);

    const changeB = document.createElement("button");
    changeB.dataset.id = contact.id;
    changeB.textContent = "Change";
    card.appendChild(changeB);

    const removeB = document.createElement("button");
    removeB.dataset.id = contact.id;
    removeB.textContent = "Remove";
    card.appendChild(removeB);

    contactGrid.appendChild(card)
  });
}
