"use client";

import { InputAdornment, Stack, TextField, Typography } from "@mui/material";
import { SearchOutlined as SearchIcon } from "@mui/icons-material";

const FilterSection = () => {
  return (
    <Stack width="var(--filter-section-width)">
      <TextField
        variant="outlined"
        InputProps={{
          disableUnderline: true,
          startAdornment: (
            <InputAdornment position="start">
              <SearchIcon />
            </InputAdornment>
          ),
        }}
      />

      <Typography>test</Typography>
    </Stack>
  );
};

export default FilterSection;
