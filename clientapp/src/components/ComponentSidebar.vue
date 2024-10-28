<template>
  <nav id="sidebarMenu" class="sidebar d-md-block sidebar-sticky px-3" style="background-color: #081028">
    <div class="position-sticky">
      <ul class="nav flex-column">
        <!-- Logo -->
        <li class="nav-item p-3 text-center">
          <a href="#" class="navbar-brand">
            <h2 class="d-block mt-2 text-light">Administrator</h2>
          </a>
        </li>

        <!-- Dashboard -->
        <li class="nav-item">
          <a class="nav-link active" href="#">
            <i class="fas fa-fw fa-tachometer-alt" style="color: #007bff;"></i>
            Dashboard
          </a>
        </li>

        <!-- Customers Dropdown -->
        <li class="nav-item">
          <a class="nav-link" @click="toggleCustomersDropdown" :class="{ 'active': isCustomersDropdownOpen }">
            <i class="fas fa-fw fa-user" style="color: #28a745;"></i>
            Customers
            <i class="fas fa-chevron-down" style="margin-left: auto;"></i> <!-- Arrow icon -->
          </a>
          <ul v-if="isCustomersDropdownOpen" class="nav flex-column ms-3">
            <li class="nav-item">
              <router-link to="/dashboard/customer-create" class="nav-link"><i class="fas fa-plus" style="color: #28a745;"></i>Create</router-link>
            </li>
            <li class="nav-item">
               <router-link to="/dashboard/customer-update" class="nav-link"><i class="fas fa-edit" style="color: #007bff;"></i>Update</router-link>
            </li>
            <li class="nav-item">
               <router-link to="/dashboard/customer-delete" class="nav-link"><i class="fas fa-trash" style="color: #dc3545;"></i>Delete</router-link>
            </li>
            <li class="nav-item">
               <router-link to="/dashboard/customer-list" class="nav-link"><i class="fas fa-list" style="color: #17a2b8;"></i>List</router-link>
            </li>
          </ul>
        </li>
        <li class="nav-item">
          <a class="nav-link" @click="toggleSuppliersDropdown" :class="{ 'active': isSuppliersDropdownOpen }">
            <i class="fas fa-fw fa-truck" style="color: #6f42c1;"></i>
            Suppliers
            <i class="fas fa-chevron-down" style="margin-left: auto;"></i> <!-- Arrow icon -->
          </a>
          <ul v-if="isSuppliersDropdownOpen" class="nav flex-column ms-3">
            <li class="nav-item">
              <router-link to="/dashboard/supplier-create" class="nav-link"><i class="fas fa-plus" style="color: #28a745;"></i>Create</router-link>
            </li>
            <li class="nav-item">
              <router-link to="/dashboard/supplier-update" class="nav-link"><i class="fas fa-edit" style="color: #007bff;"></i>Update</router-link>
            </li>
            <li class="nav-item">
              <router-link to="/dashboard/supplier-delete" class="nav-link"><i class="fas fa-trash" style="color: #dc3545;"></i>Delete</router-link>
            </li>
            <li class="nav-item">
              <router-link to="/dashboard/supplier-list" class="nav-link"><i class="fas fa-list" style="color: #17a2b8;"></i>List</router-link>
            </li>
          </ul>
        </li>

      <li class="nav-item">
        <a class="nav-link" @click="toggleInvoicesDropdown" :class="{ 'active': isInvoicesDropdownOpen }">
          <i class="fas fa-fw fa-file-invoice" style="color: #ffc107;"></i>
          Invoices
          <i class="fas fa-chevron-down" style="margin-left: auto;"></i>
        </a>
        <ul v-if="isInvoicesDropdownOpen" class="nav flex-column ms-3">
          <li class="nav-item">
            <a class="nav-link" @click="toggleCreateDropdown" :class="{ 'active': isCreateDropdownOpen }">
              <i class="fas fa-plus" style="color: #28a745;"></i> Create
              <i class="fas fa-chevron-down" style="margin-left: auto;"></i>
            </a>
            <ul v-if="isCreateDropdownOpen" class="nav flex-column ms-3">
              <li class="nav-item">
                <router-link to="/dashboard/invoice-create/sale" class="nav-link">
                  <i class="fas fa-receipt" style="color: #28a745;"></i> Sale
                </router-link>
              </li>
              <li class="nav-item">
                <router-link to="/dashboard/invoice-create/purchase" class="nav-link">
                  <i class="fas fa-shopping-cart" style="color: #28a745;"></i> Purchase
                </router-link>
              </li>
              <li class="nav-item">
                <router-link to="/dashboard/invoice-create/sale-return" class="nav-link">
                  <i class="fas fa-undo" style="color: #dc3545;"></i> Sale Return
                </router-link>
              </li>
              <li class="nav-item">
                <router-link to="/dashboard/invoice-create/purchase-return" class="nav-link">
                  <i class="fas fa-undo" style="color: #dc3545;"></i> Purchase Return
                </router-link>
              </li>
            </ul>
          </li>
          <li class="nav-item">
            <router-link to="/dashboard/invoice-update" class="nav-link">
              <i class="fas fa-edit" style="color: #007bff;"></i> Update
            </router-link>
          </li>
          <li class="nav-item">
            <router-link to="/dashboard/invoice-delete" class="nav-link">
              <i class="fas fa-trash" style="color: #dc3545;"></i> Delete
            </router-link>
          </li>
          <li class="nav-item">
            <router-link to="/dashboard/invoice-list" class="nav-link">
              <i class="fas fa-list" style="color: #17a2b8;"></i> List
            </router-link>
          </li>
        </ul>
      </li>

        <li class="nav-item">
          <a class="nav-link" @click="toggleStocksDropdown" :class="{ 'active': isStocksDropdownOpen }">
            <i class="fas fa-fw fa-boxes" style="color: #8e1128;"></i>
            Stocks
            <i class="fas fa-chevron-down" style="margin-left: auto;"></i>
          </a>
          <ul v-if="isStocksDropdownOpen" class="nav flex-column ms-3">
            <li class="nav-item">
              <router-link to="/dashboard/stock-create" class="nav-link"><i class="fas fa-plus" style="color: #28a745;"></i>Create</router-link>
            </li>
            <li class="nav-item">
              <router-link to="/dashboard/stock-update" class="nav-link"><i class="fas fa-edit" style="color: #007bff;"></i>Update</router-link>
            </li>
            <li class="nav-item">
              <router-link to="/dashboard/stock-delete" class="nav-link"><i class="fas fa-trash" style="color: #dc3545;"></i>Delete</router-link>
            </li>
            <li class="nav-item">
              <router-link to="/dashboard/stock-list" class="nav-link"><i class="fas fa-list" style="color: #17a2b8;"></i>List</router-link>
            </li>
          </ul>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="#">
            <i class="fas fa-fw fa-sign-in-alt" style="color: #007bff;"></i>
            Login
          </a>
        </li>
      </ul>
    </div>
  </nav>
</template>

<script>
export default {
  name: "ComponentSidebar",
  data() {
    return {
      isCustomersDropdownOpen: false,
      isSuppliersDropdownOpen: false,
      isInvoicesDropdownOpen: false,
      isStocksDropdownOpen: false,
      isCreateDropdownOpen: false,
    };
  },
  methods: {
    toggleCustomersDropdown() {
      this.isCustomersDropdownOpen = !this.isCustomersDropdownOpen;
    },
    toggleSuppliersDropdown() {
      this.isSuppliersDropdownOpen = !this.isSuppliersDropdownOpen;
    },
    toggleStocksDropdown() {
      this.isStocksDropdownOpen = !this.isStocksDropdownOpen;
    },
    toggleInvoicesDropdown() {
      this.isInvoicesDropdownOpen = !this.isInvoicesDropdownOpen;
    },
    toggleCreateDropdown() {
      this.isCreateDropdownOpen = !this.isCreateDropdownOpen;
    }
  }
}
</script>

<style scoped>
.sidebar {
  height: 100vh;
  width: 250px; 
  top: 0;
  left: 0;
  padding-top: 20px;
  font-size: medium;
  background-color: #020716;
}
.nav-link {
  background-color: #081028;
  color: #dfdfdf;
}
.nav-link:hover {
  background-color: #040404;
  color: #d5d5d5;
  cursor: pointer;
}
.nav-link i {
  margin-right: 10px;
}
.ms-3 {
  margin-left: 1rem;
}
</style>
