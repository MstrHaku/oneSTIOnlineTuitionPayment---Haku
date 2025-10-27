console.log("This is paymaya");


// CHECK IF AMOUNT EXCEEDED THE AVAILABLE BALANCE
const amountError = document.getElementById("amountError");
const paymentFormCon = document.getElementById("paymentFormCon");
const paymayaBtn = document.getElementById("paymaya");

paymentFormCon.addEventListener("submit", async function (event) {
    event.preventDefault();
    amountError.textContent = "";

    const form = event.target;
    const totalAmount = parseInt(document.getElementById("totalAmount").value) || 0;

    try {
        const response = await fetch(form.action, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                Amount: totalAmount
            })
        });

        const data = await response.json();

        if (!response.ok) {
            console.warn("Server error: ", data);
            amountError.textContent = data?.message || "Amount exceeded!";
            paymayaBtn.innerHTML = "Go back to merchant";
            const url = paymayaBtn.dataset.url;
            paymayaBtn.addEventListener("click", () => {
                location.href = url;
            });
         } else if (data.url) {
            window.location.href = data.url;
         }
    } 
    catch (err) {
        amountError.textContent = "Network error occured. Please try again later.";
    }
});
