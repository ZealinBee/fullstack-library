import React, {useEffect} from 'react'

import Header from '../components/Header'
import useAppDispatch from '../redux/hooks/useAppDispatch'
import useAppSelector from '../redux/hooks/useAppSelector'
import { getUserProfile, deleteProfile } from '../redux/reducers/usersReducer'

function ProfilePage() {
  const user = useAppSelector(state => state.users.currentUser)
  const dispatch = useAppDispatch()
  let token = useAppSelector(state => state.users.currentToken)

  useEffect(() => {
    dispatch(getUserProfile(token))
  }, [])


  return (
    <div>
      <Header></Header>
      <h1>Profile Page</h1>
      <h2>{user?.firstName}</h2>
      <h2>{user?.lastName}</h2>
      <h2>{user?.email}</h2>
      <h2>{user?.role}</h2>
      <button onClick={() => dispatch(deleteProfile(token))}>Delete account</button>
      <button >Update account</button>
    </div>
  )
}

export default ProfilePage