import React from 'react'

import CreateBook from '../components/CreateBook'
import UserList from '../components/UserList'
import Header from '../components/Header'

function DashboardPage() {
  return (
    <div>
      <Header />
      <h1>Dashboard</h1>
      <CreateBook />
      <UserList />
    </div>
  )
}

export default DashboardPage