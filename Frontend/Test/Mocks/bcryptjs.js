const bcryptMock = {
    hash: jest.fn((password, saltRounds) => Promise.resolve(`hashed-${password}`))
  };
  
  export default bcryptMock;