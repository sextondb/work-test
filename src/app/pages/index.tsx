import { useAuth } from '../utils/auth';
import Dashboard from '../components/Dashboard';

const IndexPage = () => {
  const userIsLoggedIn = useAuth();
  if (!userIsLoggedIn) {
    return null;
  }
  return <Dashboard></Dashboard>;
}

export default IndexPage;
