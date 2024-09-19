# Promotion program

- With any promotion program:
  - Has status: NEW, IN_PROGRESS, FINISHED
  - If status is NEW, allow to modify program: edit, active, inactive
  - With IN_PROGRESS and FINISHED program, do not allow to change anything

## Voucher

Apply voucher for all products in shop

- Name
- Code (SHOP_CODE + VOUCHER_CODE)
- Start time - end time
- Voucher type: discount, point_back
- Discount type: amount, percentage (with percentage, allow to set max)
- Min spend (giá trị đơn hàng tối thiểu)
- Max Redemptions
- Max Redemptions Per User
- Display setting: web, mobile, all, none
- Products applied type: all products, specified products
- Customer applied type: new, old, all

Bonus: 
- Allow to save voucher before (set duration to allow customer to save vouchers in customer voucher list)

## Program

- Name
- Start time - end time
- Product list: per product
  - Active or inactive product (per variants if any)
  - Set discount price for product with rule (per variants if any)
  - Max redemptions per product (not for variant if any)
  - Max redemptions per user for product (not for variant if any)

### Rule: 
- Max discount for product is 50% of list price (except for flash sale)
- Do not edit list price or add/remove variant in product that is in any programs

Note: 
- List price is price that is set when create product
- Flash sale is a special type of program with a short duration (normally 2 hours). Display a duration list in day for customer to choose

### Dev note: 
- Consider flash sale is a special type of program
- If flash sale is a special type, it is better to set maximum number of products in flash sale

## Combo

- If discount by percentage, percent is calculate in priority: sale price (if there are flash sale, sale program), list price.
- Reference to product (not variant)

### For example: 
- Buy in range: 2-3 discount amount or percentage

## Buy with deal

- Reference to product (not variant)
- have 2 type: buy_with_deal, buy_with_gift
- conditions: 
 - buy_with_gift: min spend to get max gift
 - buy with deal: max number of sub products to buy with main products

### Note:
- for products is set as gift or sub products. reference to variant in product if any

### For example: 
- Buy A get B
- Buy A get B with cheaper price


## Dev note: 
- can not create combo or buy with deal for product in flash sale
- can not create combo for product in buy with deal and vice versa

## Price calculation formula: