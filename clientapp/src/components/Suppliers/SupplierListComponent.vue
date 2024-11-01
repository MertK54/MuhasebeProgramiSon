<template>
    <table class="table table-dark">
        <thead>
            <tr>
                <th>Name</th>
                <th>Email</th>
                <th>Phone</th>
                <th>Address</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="supplier in suppliers" :key="supplier.supplier_id"> 
                <td>{{ supplier.name }}</td>
                <td>{{ supplier.e_mail }}</td>
                <td>{{ supplier.phone_number }}</td>
                <td>{{ supplier.adress }}</td>
            </tr>
        </tbody>
    </table>
    </template>
    
    <script>
    import axios from 'axios';
    import swal from 'sweetalert';
    
    export default {
        name: "SupplierListComponent",
        data() {
            return {
                suppliers: []
            }
        },
        mounted() {
            this.getSupplier();
        },
        methods: {
            getSupplier() {
                axios.get('http://localhost:5280/api/supplier/supplier-get')
                .then(response => {
                    console.log(response.data);
                    if (response != null && response.data != null) {
                        this.suppliers = response.data;
                    }
                })
                .catch(error => {
                    console.error("Error while fetching suppliers:", error);
                    swal({
                        title: "Supplier not listed",
                        text: "Error when listing suppliers",
                        icon: "warning",
                        dangerMode: true
                    });
                });
            }
        }
    }
    </script>
    
    <style>
    </style>
    