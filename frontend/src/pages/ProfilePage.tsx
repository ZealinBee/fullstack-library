import React, {useEffect} from 'react'
import { useNavigate } from 'react-router-dom'

import Header from '../components/Header'
import useAppDispatch from '../redux/hooks/useAppDispatch'
import useAppSelector from '../redux/hooks/useAppSelector'
import { getUserProfile, deleteProfile, logoutUser } from '../redux/reducers/usersReducer'

function ProfilePage() {
  const user = useAppSelector(state => state.users.currentUser)
  const dispatch = useAppDispatch()
  let token = useAppSelector(state => state.users.currentToken)
  const navigate = useNavigate()

  useEffect(() => {
    dispatch(getUserProfile(token))
  }, [])

async function deleteProfileHandler() {
    await dispatch(deleteProfile(token))
    navigate('/')
  }

  function logoutHandler() {
    dispatch(logoutUser())
    navigate('/')
  }

  return (
    <div className="page">
      <Header></Header>
      <h1>Profile Page</h1>
      <h2>{user?.firstName}</h2>
      <h2>{user?.lastName}</h2>
      <h2>{user?.email}</h2>
      <h2>{user?.role}</h2>
      <button onClick={deleteProfileHandler}>Delete account</button>
      <button >Update account</button>
      <button onClick={logoutHandler}>Logout</button>
    </div>
  )
}

export default ProfilePage