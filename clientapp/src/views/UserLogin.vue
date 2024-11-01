<template>
  <section class="login-section" style="background-color: #081028; color: white;">
    <div class="container min-vh-100 d-flex align-items-center justify-content-center">
      <div class="row align-items-center">
        <div class="col-12 col-md-6">
          <div class="welcome-text d-flex flex-column justify-content-center align-items-start">
            <hr class="border-primary-subtle mb-4">
            <h2 class="h1 mb-4">Welcome to the World of Accounting</h2>
            <p class="lead mb-5">You're in the right place to manage your business's financial health.</p>
            <p class="mb-5" style="height: 50px;"></p>
          </div>
        </div>
        <div class="col-12 col-md-6">
          <div class="card border-0 rounded-4" style="background-color: rgb(8, 17, 70); width: 100%;">
            <div class="card-body p-3 p-md-4 m-4">
              <div class="row">
                <div class="col-12">
                  <div class="mb-4">
                    <h3 style="color: white;">Sign In</h3>
                  </div>
                </div>
              </div>
              <form @submit.prevent="submitForm">
                <div class="row gy-3 overflow-hidden">
                  <div class="col-12">
                    <div class="form-floating mb-3">
                      <input v-model="formData.username" type="text" name="username" id="username" class="form-control" required>
                      <label for="username" class="form-label">Username</label>
                    </div>
                  </div>
                  <div class="col-12">
                    <div class="form-floating mb-3">
                      <input v-model="formData.password" type="password" class="form-control" name="password" id="password" required>
                      <label for="password" class="form-label">Password</label>
                    </div>
                  </div>
                  <div class="col-12">
                    <div class="d-grid">
                      <button class="btn btn-lg" type="submit" style="background-color: rgb(8, 17, 90); color:white;">Log in now</button>
                    </div>
                  </div>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import axios from 'axios';
import swal from 'sweetalert';
export default {
  name: 'UserLogin',
  data() {
    return {
      formData: {
        username: '',
        password: ''
      },
    };
  },
  methods: {
    submitForm() {
      const params = {
        username:this.formData.username,
        password: this.formData.password
      }
      axios.post('http://localhost:5280/api/user/login',params,{headers: {'Content-Type': 'application/json'}})
      .then(response => {
        if (response && response.data && response.data.token || response.data.redirectUrl) {
          localStorage.setItem('token', response.data.token);
          this.$router.push('/dashboard');
        }
        else {
          swal({title: "İnvailed login",text: "invailed login",icon: "warning",dangerMode: true});
        }
      })
      .catch(error => {
        console.error('error: ', error);
        swal({title: "Error when logging in",text: "Username or password incorrect",icon: "warning",dangerMode: true});
      });
    }
  }
};
</script>

<style>
html, body {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  height: 100%;
}
.container, .row {
  height: 100%;
}

.lead {
  margin-bottom: 2rem;
}

.card-body {
  padding: 3rem; 
}

button {
  padding: 1rem;
}
</style>
