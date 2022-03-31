const uri = 'api/palitems';
let pals = [];

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get results.', error));
}


function addItem() {
    const addEntryTextbox = document.getElementById('add-entry').value;
    const pal = addEntryTextbox.toLowerCase().match(/[a-z0-9]/g);

    if (pal.join('') === pal.reverse().join('')) {

        //I was not instructed to deny duplicates etc so kept simple here
        
        const item = {
            isPalindrome: false,
            entry: addEntryTextbox.trim()
        };

        fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        })
            .then(response => response.json())
            .then(() => {
                getItems();
                addEntryTextbox.value = '';
            })
            .catch(error => console.error('Unable to add item.', error));

        //I am not sure if look and feel is important or you just need to see it working, so kept things simple
        //for better UX I normally use sweetalert2 for alerts like the below.

        alert('CONGRATULATIONS! ' + addEntryTextbox + ' is a palindrome and has been added to the list');
        document.getElementById('add-entry').value = '';
    }
    else {
       // I'll leave the text in the box in case they made a small mistake);
        $('#myModal').modal('toggle');
    }
   
}

//I kept a delete function as testing is annoying sometimes without it

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}


function _displayCount(itemCount) {
    const entry = (itemCount === 1) ? 'known palindrome' : 'known palindromes';

    document.getElementById('counter').innerText = `${itemCount} ${entry}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('pals');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode1 = document.createTextNode(item.id);
        td1.appendChild(textNode1);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.entry);
        td2.appendChild(textNode);


        let td3 = tr.insertCell(2);
     /*  A simple button is:
      *  td3.appendChild(deleteButton);
      * I prefer icons like the below but with the alt for WCAG */
         $(td3).html('<input class="action_button" type="image" src="img/delete.png" alt="Delete" />');
        td3.setAttribute('onclick', `deleteItem(${item.id})`);
    });

    pals = data;
}
