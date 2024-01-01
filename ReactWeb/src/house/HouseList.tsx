import { useNavigate } from 'react-router-dom';
import ApiStatus from '../ApiStatus';
import { currencyFormatter } from '../config';
import useFetchHouses from '../hooks/HouseHooks';
import { Link } from 'react-router-dom';

const HouseList = () => {
  const nav = useNavigate();
  const { data, status, isSuccess } = useFetchHouses();

  if (!isSuccess) return <ApiStatus status={status} />;

  return (
    <div>
      <div className="row mb-2">
        <h5 className="themeFontColor text-center">Houses currently on the market</h5>
      </div>
      <table className="table table-hover">
        <thead>
          <tr>
            <th>Address</th>
            <th>Country</th>
            <th>Asking Price</th>
          </tr>
        </thead>
        <tbody>
          {data?.map((h) => (
            <tr key={h.id} onClick={() => nav(`/houses/${h.id}`)}>
              <td>{h.address}</td>
              <td>{h.country}</td>
              <td>{currencyFormatter.format(h.price)}</td>
            </tr>
          ))}
        </tbody>
      </table>
      <Link className="btn btn-primary" to="/houses/add">
        Add
      </Link>
    </div>
  );
};

export default HouseList;
