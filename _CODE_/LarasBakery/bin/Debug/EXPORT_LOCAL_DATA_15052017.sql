
INSERT INTO SALES_HEADER(ID, SALES_INVOICE, CUSTOMER_ID, SALES_DATE, SALES_TOTAL, SALES_DISCOUNT_FINAL, SALES_TOP, SALES_TOP_DATE, SALES_PAID, SALES_PAYMENT, SALES_PAYMENT_CHANGE, SALES_PAYMENT_METHOD, SALES_VOID, SALES_ACTIVE, SALES_ORDER_DELIVERED, SALES_ORDER_COMPLETED, SYNCHRONIZED, EDITED, BRANCH_ID) VALUES('1', 'SLO001-1', '1', STR_TO_DATE('15-05-2017 13:53', '%d-%m-%Y %H:%i'), '2000', '0', '1', STR_TO_DATE('15-05-2017', '%d-%m-%Y'), '1', '2000', '0', '0', '0', '1', '0', '0', '1', '0', 1);


INSERT INTO SALES_DETAIL(ID, SALES_INVOICE, PRODUCT_ID, PRODUCT_PRICE, PRODUCT_SALES_PRICE, PRODUCT_QTY, PRODUCT_DISC1, PRODUCT_DISC2, PRODUCT_DISC_RP, SALES_SUBTOTAL, SYNCHRONIZED, EDITED, BRANCH_ID) VALUES('1', 'SLO001-1', 'RT001', '10', '100', '20', '0', '0', '0', '2000', '1', '0', 1);


INSERT INTO CREDIT(CREDIT_ID, SALES_INVOICE, CREDIT_DUE_DATE, CREDIT_NOMINAL, CREDIT_PAID, SYNCHRONIZED, EDITED, BRANCH_ID) VALUES('1', 'SLO001-1', STR_TO_DATE('15-05-2017', '%d-%m-%Y'), '2000', '1', '0', '0', 1);


INSERT INTO PAYMENT_CREDIT(payment_id, credit_id, payment_date, pm_id, payment_nominal, payment_description, payment_confirmed, payment_invalid, payment_confirmed_date, payment_due_date, payment_is_dp, SYNCHRONIZED, EDITED, BRANCH_ID) VALUES('1', '1', STR_TO_DATE('15-05-2017', '%d-%m-%Y'), '1', '2000', 'PEMBAYARAN SLO001-1', '1', '0', STR_TO_DATE('15-05-2017', '%d-%m-%Y'), STR_TO_DATE('15-05-2017', '%d-%m-%Y'), '0', '0', '0', 1);


INSERT INTO DAILY_JOURNAL(journal_id, account_id, journal_datetime, journal_nominal, journal_description, user_id, pm_id, SYNCHRONIZED, EDITED, BRANCH_ID) VALUES('1', '1', STR_TO_DATE('15-05-2017 13:53', '%d-%m-%Y %H:%i'), '2000', 'PEMBAYARAN SLO001-1', '1', '1', '0', '0', 1);
INSERT INTO DAILY_JOURNAL(journal_id, account_id, journal_datetime, journal_nominal, journal_description, user_id, pm_id, SYNCHRONIZED, EDITED, BRANCH_ID) VALUES('2', '1', STR_TO_DATE('15-05-2017 13:54', '%d-%m-%Y %H:%i'), '0', 'PEMBAYARAN DP SLO001-2', '1', '1', '0', '0', 1);





