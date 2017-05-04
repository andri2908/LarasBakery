
INSERT INTO SALES_HEADER(ID, SALES_INVOICE, CUSTOMER_ID, SALES_DATE, SALES_TOTAL, SALES_DISCOUNT_FINAL, SALES_TOP, SALES_TOP_DATE, SALES_PAID, SALES_PAYMENT, SALES_PAYMENT_CHANGE, SALES_PAYMENT_METHOD, SALES_VOID, SALES_ACTIVE, BRANCH_ID) VALUES('1', 'SLO001-1', '1', STR_TO_DATE('02-05-2017 13:59', '%d-%m-%Y %H:%i'), '30000', '500', '1', STR_TO_DATE('02-05-2017', '%d-%m-%Y'), '1', '100000', '70500', '0', '0', '1', 0);


INSERT INTO SALES_DETAIL(ID, SALES_INVOICE, PRODUCT_ID, PRODUCT_PRICE, PRODUCT_SALES_PRICE, PRODUCT_QTY, PRODUCT_DISC1, PRODUCT_DISC2, PRODUCT_DISC_RP, SALES_SUBTOTAL, BRANCH_ID) VALUES('1', 'SLO001-1', 'RT001', '10', '1000', '20', '0', '0', '0', '20000', 0);
INSERT INTO SALES_DETAIL(ID, SALES_INVOICE, PRODUCT_ID, PRODUCT_PRICE, PRODUCT_SALES_PRICE, PRODUCT_QTY, PRODUCT_DISC1, PRODUCT_DISC2, PRODUCT_DISC_RP, SALES_SUBTOTAL, BRANCH_ID) VALUES('2', 'SLO001-1', 'RT002', '200', '2000', '5', '0', '0', '0', '10000', 0);


INSERT INTO CREDIT(CREDIT_ID, SALES_INVOICE, CREDIT_DUE_DATE, CREDIT_