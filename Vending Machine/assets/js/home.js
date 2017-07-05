$(document).ready(function () {
    var itemSelected = 0;
    var totalDeposited = 0;
    loadProducts();
    $('#changeReturn').hide();

//Add money to Vending Machine
    $('#dollar').click(function (event){
        Number(totalDeposited += 1.00);
        addChange(totalDeposited);
    });

    $('#quarter').click(function (event){
        Number(totalDeposited += 0.25);
        addChange(totalDeposited);
    });

    $('#dime').click(function (event){
        Number(totalDeposited += 0.10);
        addChange(totalDeposited);
    });

    $('#nickle').click(function (event){
        Number(totalDeposited += 0.05);
        addChange(totalDeposited);
    });
//Keeps Track of Total Change Added
    function addChange(totalDeposited){
        $('#money').empty();
        $('#change').empty();
        $('#responseMessage').empty();
        $('#money').append(totalDeposited.toFixed(2));
        $('#changeReturn').show();
    }
//Returns Change To User If Aborted
    $('#changeReturn').click(function (event){
        $('#itemNumber').empty();
        $('#money').empty();
        $('#responseMessage').empty();
        Number(itemSelected = 0);
        Number(totalDeposited = 0);
    });
//Choose Item
    $('#item1').click(function (event){
        $('#responseMessage').empty();
        Number(itemSelected = 1);
        $('#itemNumber').empty();
        $('#itemNumber').append(itemSelected);
    });
    $('#item2').click(function (event){
        $('#responseMessage').empty();
        Number(itemSelected = 2);
        $('#itemNumber').empty();
        $('#itemNumber').append(itemSelected);
    });
    $('#item2').click(function (event){
        $('#responseMessage').empty();
        Number(itemSelected = 2);
        $('#itemNumber').empty();
        $('#itemNumber').append(itemSelected);
    });
    $('#item3').click(function (event){
        $('#responseMessage').empty();
        Number(itemSelected = 3);
        $('#itemNumber').empty();
        $('#itemNumber').append(itemSelected);
    });
    $('#item4').click(function (event){
        $('#responseMessage').empty();
        Number(itemSelected = 4);
        $('#itemNumber').empty();
        $('#itemNumber').append(itemSelected);
    });
    $('#item5').click(function (event){
        $('#responseMessage').empty();
        Number(itemSelected = 5);
        $('#itemNumber').empty();
        $('#itemNumber').append(itemSelected);
    });
    $('#item6').click(function (event){
        $('#responseMessage').empty();
        Number(itemSelected = 6);
        $('#itemNumber').empty();
        $('#itemNumber').append(itemSelected);
    });
    $('#item7').click(function (event){
        $('#responseMessage').empty();      
        Number(itemSelected = 7);
        $('#itemNumber').empty();
        $('#itemNumber').append(itemSelected);
    });
    $('#item8').click(function (event){
        $('#responseMessage').empty();
        Number(itemSelected = 8);
        $('#itemNumber').empty();
        $('#itemNumber').append(itemSelected);
    });
    $('#item9').click(function (event){
        $('#responseMessage').empty();
        Number(itemSelected = 9);
        $('#itemNumber').empty();
        $('#itemNumber').append(itemSelected);
    });

//Purchase An Item
    $('#purchase').click(function (event){
        $.ajax({
            type: 'GET',
            url: `http://localhost:8080/money/${totalDeposited}/item/${itemSelected}`,
            success: function(data, status) {
                    var quarters = data.quarters;
                    var dimes = data.dimes;
                    var nickels = data.nickels;
                    var pennies = data.pennies;
                    if(data.quarters > 0){
                        $('#change').append(`${data.quarters} Quarters <br />`);
                    }

                    if(data.dimes > 0){
                        $('#change').append(`${data.dimes} Dimes <br />`);
                    }

                    if(data.nickels > 0){
                        $('#change').append(`${data.nickels} Nickels <br />`);
                    }
                    
                    if(data.pennies > 0){
                        $('#change').append(`${data.pennies} Pennies `);
                    }
                    $('#responseMessage').empty();
                    clearProducts(data);  
                    loadProducts();
                    Number(itemSelected = 0);
                    Number(totalDeposited = 0);
                    resetSuccess();
                    $('#changeReturn').hide();

                },
                error: function(data){
                    var errors = data.responseJSON;
                    console.log(errors);
                    if(errors.message == 'No message available')
                    {
                        errors.message = 'Please select an Item!';
                    }
                    $('#responseMessage').empty();
                    $('#responseMessage').append(errors.message);
                }
                });
            })
        });

// Load Candies
function loadProducts(){
    var i = 1;
    $.ajax ({
        type: 'GET',
        url: 'http://localhost:8080/items',
        success: function(data, status) {
            clearProducts(data);
            $.each(data, function(index, product) {
                var id = 'item' + i + 'Id';
                var name = 'item' + i + 'Name';
                var price = 'item' + i + 'Price';
                var qty = 'item' + i + 'Qty';

                $('#' + id).append(product.id);
                $('#' + name).append(product.name);
                $('#' + price).append(`$ ${product.price.toFixed(2)}`);
                $('#' + qty).append(`Quantity: ${product.quantity}`);

                i++;
            });
        },
        error: function() {
            $('#errorMessages').attr({class: 'list-group-item list-group-item-danger'}).text('Error calling web service. Please Try Again Later.')
        }
    });
}

function clearProducts(data){
    var i = 1;
    $.each(data, function(index, product) {
        var id = 'item' + i + 'Id';
        var name = 'item' + i + 'Name';
        var price = 'item' + i + 'Price';
        var qty = 'item' + i + 'Qty';

        $('#' + id).empty();
        $('#' + name).empty();
        $('#' + price).empty();
        $('#' + qty).empty();

        i++;
    });
};

function resetSuccess(){
    $('#itemNumber').empty();
    $('#money').empty();
    $('#responseMessage').append('Thank You!!')
}