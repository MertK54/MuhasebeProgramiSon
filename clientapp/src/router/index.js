import { createRouter, createWebHistory } from 'vue-router';
import UserLogin from '../views/UserLogin.vue'
import DashboardView from '../views/DashboardView.vue'
import CustomerListComponent from '../components/Customers/CustomerListComponent.vue';
import CustomerCreateComponent from '@/components/Customers/CustomerCreateComponent.vue';
import CustomerUpdateComponent from '@/components/Customers/CustomerUpdateComponent.vue';
import CustomerDeleteComponent from '@/components/Customers/CustomerDeleteComponent.vue';

import SupplierCreateComponent from '@/components/Suppliers/SupplierCreateComponent.vue';
import SupplierListComponent from '@/components/Suppliers/SupplierListComponent.vue';
import SupplierUpdateComponent from '@/components/Suppliers/SupplierUpdateComponent.vue';
import SupplierDeleteComponent from '@/components/Suppliers/SupplierDeleteComponent.vue';

import StockCreateComponent from '@/components/Stocks/StockCreateComponent.vue';
import StockListComponent from '@/components/Stocks/StockListComponent.vue';
import StockUpdateComponent from '@/components/Stocks/StockUpdateComponent.vue';
import StockDeleteComponent from '@/components/Stocks/StockDeleteComponent.vue';

import InvoiceSaleComponent from '@/components/Invoices/InvoiceSaleComponent.vue';
import InvoicePurchaseComponent from '@/components/Invoices/InvoicePurchaseComponent.vue';
import InvoiceSaleReturnComponent from '@/components/Invoices/InvoiceSaleReturnComponent.vue';
const routes = [
  {
    path: '/login',
    name: 'UserLogin',
    component: UserLogin
  },
  {
    path: '/dashboard',
    name: 'DashboardView',
    component: DashboardView,
    meta:{requiresAuth:true},
    children:[
      {
        path:'customer-list',
        component: CustomerListComponent
      },
      {
        path:'customer-create',
        component:CustomerCreateComponent
      },
      {
        path:'customer-update',
        component:CustomerUpdateComponent
      },
      {
        path:'customer-delete',
        component:CustomerDeleteComponent
      },
      {
        path:'supplier-create',
        component:SupplierCreateComponent
      },
      {
        path:'supplier-list',
        component:SupplierListComponent
      },
      {
        path:'supplier-update',
        component:SupplierUpdateComponent
      },
      {
        path:'supplier-delete',
        component:SupplierDeleteComponent
      },
      {
        path:'stock-create',
        component:StockCreateComponent
      },
      {
        path:'stock-list',
        component:StockListComponent
      },
      {
        path:'stock-update',
        component:StockUpdateComponent
      },
      {
        path:'stock-delete',
        component:StockDeleteComponent
      },
      {
        path:'invoice-create/sale',
        component:InvoiceSaleComponent
      },
      {
        path:'invoice-create/purchase',
        component:InvoicePurchaseComponent
      },
      {
        path:'invoice-create/sale-return',
        component:InvoiceSaleReturnComponent
      }
    ]
  }
];
const router = createRouter({
  history: createWebHistory(),
  routes,
});
router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('token');
  if (!token && to.name !== 'UserLogin') {
    next({ name: 'UserLogin' });
  } 
  else if (token && to.name === 'UserLogin') {
    next({ name: 'DashboardView' });
  }
  else{
    next();
  }
});


export default router;
