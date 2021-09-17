import React, { Component } from 'react';
import authService from '../../components/api-authorization/AuthorizeService';

export class EmployeesIndex extends Component {
  static displayName = EmployeesIndex.name;

  constructor(props) {
    super(props);
    this.state = { employees: [], loading: true };
  }

  componentDidMount() {
    this.populateEmployeeData();
  }

  static renderEmployeesTable(employees,parent) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Full Name</th>
            <th>Birthdate</th>
            <th>TIN</th>
            <th>Type</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {employees.map(employee =>
            <tr key={employee.id}>
              <td>{employee.Fullname}</td>
              <td>{employee.Birthdate}</td>
              <td>{employee.TIN}</td>
              <td>{employee.EmployeeTypeId === "1"?"Regular":"Contractual"}</td>
              <td>
              <button type='button' className='btn btn-info mr-2' onClick={() => parent.props.history.push("/employees/" + employee.id + "/edit")} >Edit</button>
              <button type='button' className='btn btn-primary mr-2' onClick={() => parent.props.history.push("/employees/" + employee.id + "/calculate")}>Calculate</button>
            <button type='button' className='btn btn-danger mr-2' onClick={() => {
              if (window.confirm("Are you sure you want to delete?")) {
                parent.deleteEmployee(employee.id);
              } 
            } }>Delete</button></td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : EmployeesIndex.renderEmployeesTable(this.state.employees,this);

    return (
      <div>
        <h1 id="tabelLabel" >Employees</h1>
        <p>This page should fetch data from the server.</p>
        <p><button type='button' className='btn btn-success mr-2' onClick={() => this.props.history.push("/employees/create")} >Create</button></p>
        {contents}
      </div>
    );
  }



  async populateEmployeeData() {
      const token = await authService.getAccessToken();
      const axios = require('axios').default;
      axios.get("api/employees", { headers: !token ? {} : { 'Authorization': `Bearer ${token}` } }).then(res => {
          this.setState({ employees: res.data, loading: false });
      })

    //  const response = await fetch('api/employees', {
    //  headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    //});
    //const data = await response.json();
    //this.setState({ employees: data, loading: false });
  }

  async deleteEmployee(id) {
    const token = await authService.getAccessToken();
    const requestOptions = {
        method: 'DELETE',
        headers: !token ? {} : { 'Authorization': `Bearer ${token}`,'Content-Type': 'application/json' }
      };

       const axios = require('axios').default;
      axios.delete(`api/employees/delete/user/${id}`, requestOptions).then(res => {
           if (res.status === 200) {
               this.populateEmployeeData();
           }
           else {
               alert("There was an error occured.")
           }
           }).catch(err => {

               alert("There was an error occured.")
           });
     
  }
}
