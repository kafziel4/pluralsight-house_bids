import { useMutation, useQuery, useQueryClient } from 'react-query';
import { Bid } from '../types/bid';
import Problem from '../types/problem';
import config from '../config';
import axios, { AxiosError, AxiosResponse } from 'axios';

const useFetchBids = (houseId: number) => {
  return useQuery<Bid[], AxiosError<Problem>>(['bids', houseId], () =>
    axios.get(`${config.baseApiUrl}/houses/${houseId}/bids`).then((resp) => resp.data)
  );
};

const useAddBid = () => {
  const queryClient = useQueryClient();
  return useMutation<AxiosResponse, AxiosError<Problem>, Bid>(
    (b) => axios.post(`${config.baseApiUrl}/houses/${b.houseId}/bids`, b),
    {
      onSuccess: (_, bid) => {
        queryClient.invalidateQueries(['bids', bid.houseId]);
      },
    }
  );
};

export { useFetchBids, useAddBid };
