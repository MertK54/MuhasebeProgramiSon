<template>
    <nav class="navbar navbar-expand-lg" style="background-color: #081028;">
      <div class="container-fluid">
        <!-- Hamburger Menu for Sidebar (optional) -->
        <button class="navbar-toggler" type="button" data-bs-toggle="offcanvas" data-bs-target="#sidebarMenu" aria-controls="sidebarMenu">
          <span class="navbar-toggler-icon"></span>
        </button>
  
        <div class="d-flex ms-3 text-light">
          <h1>Welcome.</h1>
        </div>
  
        <!-- Right Aligned Icons -->
        <div class="d-flex align-items-center ms-auto">

          <div class="dropdown">
            <button class="btn btn-link dropdown-toggle" type="button" id="userMenuButton" data-bs-toggle="dropdown" aria-expanded="false">
              <img src="https://via.placeholder.com/40" class="rounded-circle" alt="User Avatar">
              <span class="ms-2">John Doe</span>
            </button>
            <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="userMenuButton">
              <li><a class="dropdown-item" href="#">Profile</a></li>
              <li><a class="dropdown-item" href="#">Settings</a></li>
              <li><hr class="dropdown-divider"></li>
              <li><button class="btn dropdown-item" href="#" @click="logoutUser">Logout</button></li>
            </ul>
          </div>
        </div>
      </div>
    </nav>
  </template>
  
  <script>
import axios from 'axios';
import swal from 'sweetalert';
  export default {
    name: "ComponentsTop",
    data(){
      return{
        token : null
      }
    },
    methods:{
      logoutUser(){
        this.token = localStorage.getItem('token');
        axios.post('http://localhost:5280/api/user/logout', 
        { token: this.token }, 
        { headers: { 'Content-Type': 'application/json' } })
        .then(response => {
          if (response.data.token) {
            swal({
              title: "Successfully logout",
              icon: "success",
                    });
            localStorage.removeItem('token');
            this.$router.push('/login');
          }
        })
        .catch(error => {
        console.error('error: ', error);
        swal({
              title: "Error when logout in",
              text: "Error when logout",
              icon: "warning",
              dangerMode: true
                    });
      })
    }
  }}
  </script>
  
  <style scoped>
  .navbar {
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  }
  
  .navbar .form-control {
    width: 250px;
  }
  
  .badge {
    font-size: 0.8rem;
    position: absolute;
    top: 0;
    right: 0;
    transform: translate(50%, -50%);
  }
  
  img.rounded-circle {
    width: 40px;
    height: 40px;
    object-fit: cover;
  }
  </style>
  