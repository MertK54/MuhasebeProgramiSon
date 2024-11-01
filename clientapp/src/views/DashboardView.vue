<template>
    <div class="d-flex vh-100">
      <ComponentSidebar class="sidebar" style="border-right: 1px solid rgba(249, 247, 247, 0.2)" />
      <div class="flex-grow-1 d-flex flex-column " style="background-color: #081028;">
        <ComponentsTop class="topbar" style="border-bottom: 1px solid rgba(249, 247, 247, 0.2)" />
        <div class="flex-grow-1 p-3 text-white">
          <router-view class="flex-grow-1 p-3 text-white"></router-view>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import ComponentSidebar from '@/components/ComponentSidebar.vue';
  import ComponentsTop from '@/components/ComponentsTop.vue';
  export default {
    data() {
      return {
      };
    },
    components: {
      ComponentSidebar,
      ComponentsTop,
    },
  };
  </script>
  
  <style scoped>
    body {
      background-color: #081028;
      color: rgb(232, 228, 228);
    }
    .topbar {
      background-color: #040404;
      padding: 10px;
      box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
    }
    .nav-link:hover {
      background-color: #495057;
      cursor: pointer;
    }
    .vh-100 {
      height: 100vh;
    }
    .flex-grow-1 {
      padding: 15px;
    }
  </style>
  