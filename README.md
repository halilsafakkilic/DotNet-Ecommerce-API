# E-Commerce API with Ocelot
This project runs on **.Net 6** with **Ocelot** and on **Docker**(+Compose) with **MongoDB**. Therefore, you must have .Net 6 and Docker(+Compose) installed on your device.

&nbsp;
## Installation
```
cd docker; docker-compose up
```
ApiGateway, CartAPI and ProductAPI must run at the same time to run the project.

*Includes tiny unit tests in CartAPIUnitTest.*

&nbsp;
## Service List
If you are using *Postman*, you can import **ECommerceCartAPI.postman_collection.json**.

&nbsp;
### Product
**List**: http://localhost:7000/api/product/find

**Find By SKU**: http://localhost:7000/api/product/findBySKU/{SKU}

### Cart
**List**: http://localhost:7000/api/cart/list

**Detail**: http://localhost:7000/api/cart/detail?sku={SKU}

**Add**: *[PUT]* http://localhost:7000/api/cart/add ```{"sku": "computer", "quantity": 2}```

**Delete**: *[DELETE]* http://localhost:7000/api/cart/delete?sku={SKU}

&nbsp;
### Defined Product List
```
SKU - Quantity
toy: 5
phone: 2
computer: 4
bicycle: 3
```